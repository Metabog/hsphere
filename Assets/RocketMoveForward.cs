using UnityEngine;
using System.Collections;

public class RocketMoveForward : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += this.transform.forward * 4.0f;
	}
}
