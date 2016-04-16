using UnityEngine;
using System.Collections;

public class CameraLookat : MonoBehaviour {

	// Use this for initialization
	//Vector3 oldPos;
	Quaternion oldQuat;
	void Start () {
		//oldPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {


	}

	void FixedUpdate()
	{


		//AIMING RAYCAST, SHOULD GET THIS FROM THE PLAYER INSTEAD OF RECALC HERE
		Ray camRay = GameObject.Find ("MainCam").GetComponent<Camera>().ScreenPointToRay (Input.mousePosition);

		GameObject[] players = GameObject.FindGameObjectsWithTag ("PLAYER");

		if (players.Length == 0)
			return;

		GameObject localPlayer = null;
		Transform dummyTrans = null;

		for (int i =0; i<players.Length; i++) {
			if( players[i].GetComponent<PlayerMovement>().isLocalPlayer)
			{
				localPlayer = players[i];
				dummyTrans = localPlayer.transform.FindChild("CameraDummy");
				break;
			}
		}

		Vector3 point = camRay.origin + camRay.direction * 10000.0f;

		Ray aimRay = new Ray (localPlayer.transform.position, (point - localPlayer.transform.position).normalized);
		Debug.DrawRay (aimRay.origin, aimRay.direction*30.0f, Color.blue);
		/////////////////////////////////////////////////////////////////////////

		this.transform.position = Vector3.Lerp (this.transform.position, dummyTrans.position, 0.1f );
		this.transform.rotation = Quaternion.Lerp (this.transform.rotation, dummyTrans.rotation,  0.1f);

		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.FromToRotation (transform.forward, aimRay.direction) * transform.rotation, 0.1f);


	}

}
