﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

	GameObject hsphere;

	// Use this for initialization
	void Start () {
		//this.GetComponent<Rigidbody> ().freezeRotation = true;
		this.GetComponent<Rigidbody> ().angularDrag = 2.0f;
		hsphere = GameObject.Find ("hsphere_collider");
	}
	

	// Update is called once per frame
	void Update () {

		print ("update function called.");
	}

	void FixedUpdate()
	{
	
		if (!isLocalPlayer)
			return;

		MeshCollider hsphCollider = hsphere.GetComponent<MeshCollider> ();

		bool isTouchingSurface = false;
		Ray downRay = new Ray (transform.position, -transform.up);
		RaycastHit rcInfo;

		isTouchingSurface = hsphCollider.Raycast (downRay, out rcInfo, 10.0f);

		//AIMING RAYCAST, SHOULD GET THIS FROM THE PLAYER INSTEAD OF RECALC HERE
		Ray camRay = GameObject.Find ("MainCam").GetComponent<Camera>().ScreenPointToRay (Input.mousePosition);
		
		Vector3 point = camRay.origin + camRay.direction * 10000.0f;
		
		Ray aimRay = new Ray (transform.position, (point - transform.position).normalized);
		Debug.DrawRay (aimRay.origin, aimRay.direction*100.0f, Color.blue);
		/////////////////////////////////////////////////////////////////////////

		//gravity vector points at object from the center of the worldsphere
		Vector3 grav = this.transform.position;
		
		//multiple by the distance from the center
		float amount = grav.magnitude/400.0f;
		amount = amount * amount;
		//amount *= 10.0f;
		
		this.GetComponent<Rigidbody>().AddForce(grav.normalized*amount);
		
		//orient upwards
		
		Plane plane = new Plane (-grav.normalized, grav);
		

		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.FromToRotation (transform.up, plane.normal) * transform.rotation, 0.4f);
		
		if(Input.GetKey(KeyCode.W))
		{
			this.GetComponent<Rigidbody>().AddForce(transform.forward*30.0f);
		}
		
		if(Input.GetKey(KeyCode.D))
		{
			this.transform.Rotate(0.0f,2f,0.0f);
		}
		
		if(Input.GetKey(KeyCode.A))
		{
			this.transform.Rotate(0.0f,-2f,0.0f);
		}

		if(Input.GetKey(KeyCode.S))
		{
			if(isTouchingSurface)
				this.GetComponent<Rigidbody>().AddForce(-this.GetComponent<Rigidbody>().velocity.normalized*30.0f);
		}
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			this.GetComponent<Rigidbody>().AddForce(-transform.up*1000.0f);
		}

		if (Input.GetMouseButtonDown (0)) {
			GameObject newRocket = (GameObject) Instantiate(Resources.Load("RocketPrefab"), this.transform.position, Quaternion.identity);
			newRocket.transform.LookAt(aimRay.origin+ aimRay.direction*30.0f);
			newRocket.transform.localScale = new Vector3(0.7f,0.7f,0.7f);

			this.GetComponent<AudioSource>().pitch = Random.value * 0.5f + 0.75f;
			this.GetComponent<AudioSource>().Play();

		}
		
		Debug.DrawRay (this.transform.position, this.transform.forward*100.0f, Color.red);
		Debug.DrawRay (this.transform.position, this.transform.up*100.0f, Color.yellow);
	}
}
