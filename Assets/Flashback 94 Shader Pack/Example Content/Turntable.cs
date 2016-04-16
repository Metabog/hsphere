using UnityEngine;

namespace Flashback94ExampleScene {
	public class Turntable : MonoBehaviour {
		void Update () {
			if (Input.GetKey (KeyCode.Mouse0))
				this.transform.Rotate (0f, Input.GetAxis ("Mouse X") * -200f * Time.deltaTime, 0f);
		}
	}
}
