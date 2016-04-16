using UnityEngine;
using System.Collections;

public class WooshSoundController : MonoBehaviour {

	AudioSource woosh;
	float wooshage = 0.0f;
	// Use this for initialization
	void Start () {
		woosh = this.transform.FindChild ("wooshSound").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		float mag = this.GetComponent<Rigidbody> ().velocity.magnitude;
		if (mag > 100.0f)
			mag = 100.0f;

		wooshage = Mathf.Lerp (wooshage, mag, 0.3f);
		float wooshagesq = Mathf.Pow (wooshage / 100.0f, 2.0f);
		woosh.volume = wooshagesq*0.1f;
		woosh.pitch = 0.5f + wooshagesq;
		
	}
}
