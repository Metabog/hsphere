using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerAimFire : NetworkBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		/*
		if (!isLocalPlayer)
			return;

		Transform camDummy = transform.FindChild ("CameraDummy");

		Ray camRay = camDummy.GetComponent<Camera>().ScreenPointToRay (Input.mousePosition);

		Vector3 point = camRay.origin + camRay.direction * 10000.0f;
	
		Ray aimRay = new Ray (transform.position, (point - transform.position).normalized);
		Debug.DrawRay (aimRay.origin, aimRay.direction*100.0f, Color.blue);
		*/
	}
}
