using UnityEngine;

namespace Flashback94ExampleScene {
	public class SpinObject : MonoBehaviour {
		void Update () {
			this.transform.Rotate (50f * Time.deltaTime, 50f * Time.deltaTime, 50f * Time.deltaTime);
		}
	}
}
