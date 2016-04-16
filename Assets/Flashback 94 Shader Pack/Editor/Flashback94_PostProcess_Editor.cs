
//////////////////////////////////////////////////////////////////////////////////////////
//																						//
// Flashback '94 Shader Pack for Unity 3D												//
// © 2015 George Khandaker-Kokoris														//
//																						//
// Custom editor script for the 'Flashback94_PostProcess' class							//
// Must be kept in 'Editor' or one of its subdirectories								//
//																						//
//////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Flashback94_PostProcess))]
public class Flashback94_PostProcess_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		// Get the script object to edit
		Flashback94_PostProcess script = (Flashback94_PostProcess) target;
		
		// Set the color shader from a source file
		script.colorShader = (Shader) EditorGUILayout.ObjectField ("Color Shader", script.colorShader, typeof (Shader), false);
		
		// Set the bit depth of the color shader between 2 and 8
		script.bitsPerChannel = EditorGUILayout.IntSlider ("Bits Per Color Channel", script.bitsPerChannel, 2, 8);
		
		// Set the downsampling type
		script.downsampling = (Flashback94_PostProcess.DownsampleType) EditorGUILayout.EnumPopup ("Downsampling Type", script.downsampling);
		
		// Expose different properties depending on the downsampling type
		switch (script.downsampling) {
		case Flashback94_PostProcess.DownsampleType.NONE:
			// Expose no variables if we're not downsampling
			break;
		case Flashback94_PostProcess.DownsampleType.RELATIVE:
			// Relative downsampling divides the framebuffer by a positive integer between 2 and 16
			script.downsampleRelativeAmount = EditorGUILayout.IntSlider ("Downsampling Relative Amount", script.downsampleRelativeAmount, 2, 16);
			script.downsampleAntialiasing = EditorGUILayout.Toggle ("Enable Antialiasing", script.downsampleAntialiasing);
			break;
		case Flashback94_PostProcess.DownsampleType.ABSOLUTE:
			// Absolute downsampling scales the framebuffer to a fixed size between 80x45 and 1920x1080
			script.downsampleAbsoluteWidth = EditorGUILayout.IntSlider ("Downsampling Absolute Width", script.downsampleAbsoluteWidth, 80, 1920);
			script.downsampleAbsoluteHeight = EditorGUILayout.IntSlider ("Downsampling Absolute Height", script.downsampleAbsoluteHeight, 45, 1080);
			script.downsampleAntialiasing = EditorGUILayout.Toggle ("Enable Antialiasing", script.downsampleAntialiasing);
			break;
		}
	}
}
