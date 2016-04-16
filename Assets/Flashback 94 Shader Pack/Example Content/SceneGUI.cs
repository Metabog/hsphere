using UnityEngine;

namespace Flashback94ExampleScene {
	public class SceneGUI : MonoBehaviour {

		public Material cubeDiff;
		public Material cubeSpec;
		public Material cutoutDiff;
		public Material cutoutSpec;
		public Material opaqueDiff;
		public Material opaqueSpec;
		public Material opaqueUnlit;
		public Material rimDiff;
		public Material rimSpec;
		public Material transDiff;
		public Material transSpec;

		private Renderer cube;
		private Renderer capsule;

		void Start () {
			cube = GameObject.Find ("Cube").GetComponent<Renderer> ();
			capsule = GameObject.Find ("Capsule").GetComponent<Renderer> ();
		}
		
		void OnGUI () {
			GUI.Label (new Rect (10, 10, 600, 200), "FLASHBACK '94 SHADER PACK FOR UNITY\n" +
			           "© 2015 George Khandaker-Kokoris\n\nEMULATES OLD, BUSTED 3D HARDWARE LIMITATIONS:\n" +
			           "Affine texture mapping (textures warp at steep angles)\nLimited vertex precision (vertices 'snap' as they move)\n" +
			           "Reduced color depth (as low as 2 bits per channel)\nTiny framebuffer (as small as 80x45 pixels)\n\n" +
			           "Click/drag to rotate view");

			if (GUI.Button (new Rect (135, 400, 120, 30), "Opaque")) {
				cube.material = opaqueDiff;
				capsule.material = opaqueSpec;
			}

			if (GUI.Button (new Rect (260, 400, 120, 30), "Cutout")) {
				cube.material = cutoutDiff;
				capsule.material = cutoutSpec;
			}

			if (GUI.Button (new Rect (385, 400, 120, 30), "Transparent")) {
				cube.material = transDiff;
				capsule.material = transSpec;
			}

			if (GUI.Button (new Rect (135, 440, 120, 30), "Cubemap")) {
				cube.material = cubeDiff;
				capsule.material = cubeSpec;
			}

			if (GUI.Button (new Rect (260, 440, 120, 30), "Rimlight")) {
				cube.material = rimDiff;
				capsule.material = rimSpec;
			}

			if (GUI.Button (new Rect (385, 440, 120, 30), "Unlit")) {
				cube.material = opaqueUnlit;
				capsule.material = opaqueUnlit;
			}
		}
	}
}
