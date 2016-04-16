using UnityEngine;
using System.Collections;

public class ParticleExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {

		this.GetComponent<AudioSource> ().pitch = Random.value + 0.5f;
		this.GetComponent<AudioSource> ().Play ();
		Destroy (this.gameObject, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
