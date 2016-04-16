
//////////////////////////////////////////////////////////////////////////////////////////
//																						//
// Flashback '94 Shader Pack for Unity 3D												//
// © 2015 George Khandaker-Kokoris														//
//																						//
// Post-process script for scaling the framebuffer and quantizing colors				//
// Only for use with the 'Flashback 94/Image Effect/Color Quantize' shader				//
//																						//
//////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Flashback94_PostProcess : MonoBehaviour
{
	// Shader to use for color processing
	public Shader colorShader = null;
	
	// Runtime material generated from the above shader
	private Material colorMaterial = null;
	
	// Bits per color channel
	public int bitsPerChannel = 8;
	
	// Enumeration for the type of downsampling
	public enum DownsampleType { NONE, RELATIVE, ABSOLUTE };
	public DownsampleType downsampling = DownsampleType.NONE;
	
	// Scaling amount for relative downsampling
	public int downsampleRelativeAmount = 2;
	
	// Width and height for absolute downsampling
	public int downsampleAbsoluteWidth = 320;
	public int downsampleAbsoluteHeight = 240;
	
	// Enable/disable antialiasing when blitting
	public bool downsampleAntialiasing = true;
	
	void OnEnable ()
	{
		// Disable and exit if there's no shader attached
		if (!colorShader) {
			Debug.Log("<color=yellow>FLASHBACK 94 ERROR:</color> There is no shader attached to the post process!");
			enabled = false;
			return;
		}
		
		// Disable and exit if the shader is unsupported
		if (!colorShader.isSupported) {
			Debug.Log("<color=yellow>FLASHBACK 94 ERROR:</color> The shader <color=yellow>" + colorShader.name + "</color> is not supported on this platform!");
			enabled = false;
			return;
		}
		
		// Disable and exit if image effects are unsupported
		if (!SystemInfo.supportsImageEffects) {
			Debug.Log("<color=yellow>FLASHBACK 94 ERROR:</color> Image effects are not supported on this platform!");
			enabled = false;
			return;
		}
		
		// Create the material
		colorMaterial = new Material (colorShader);
		colorMaterial.hideFlags = HideFlags.DontSave;
	}
	
	void OnDisable ()
	{
		// Destroy the material
		if (colorMaterial) DestroyImmediate (colorMaterial);
	}
	
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		// Set the number of color steps in the shader
		colorMaterial.SetFloat ("_ColorSteps", Mathf.Pow (2f, bitsPerChannel));
		
		// Width and height for the buffer texture
		int bufWidth, bufHeight;
		
		// Switch statement for downsampling methods
		switch (downsampling) {
		case DownsampleType.NONE:
			// Blit directly through the color material and exit
			Graphics.Blit (source, destination, colorMaterial);
			return;
		case DownsampleType.RELATIVE:
			// Scale render texture by a relative amount
			bufWidth = source.width / downsampleRelativeAmount;
			bufHeight = source.height / downsampleRelativeAmount;
			break;
		case DownsampleType.ABSOLUTE:
			// Set render texture dimensions
			bufWidth = downsampleAbsoluteWidth;
			bufHeight = downsampleAbsoluteHeight;
			break;
		default:
			return;
		}
		
		// Create a temporary buffer and filter it by point
		RenderTexture buffer = RenderTexture.GetTemporary (bufWidth, bufHeight, 0);
		buffer.filterMode = FilterMode.Point;
		
		// Set the filtering mode of the source texture before resizing
		source.filterMode = downsampleAntialiasing ? FilterMode.Bilinear : FilterMode.Point;
		
		// Blit into the resized render texture through the color material
		Graphics.BlitMultiTap (source, buffer, colorMaterial,
		                       new Vector2 (-1f, -1f),
		                       new Vector2 (-1f, 1f),
		                       new Vector2 (1f, 1f),
		                       new Vector2 (1f, -1f)
		                       );
		
		// Blit the result to the screen and release the buffer
		Graphics.Blit (buffer, destination);
		RenderTexture.ReleaseTemporary (buffer);
	}
}
