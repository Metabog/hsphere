﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

    public GameObject bulletPrefab;
	bool isOnFloor;

	// Use this for initialization
	void Start () {
		//this.GetComponent<Rigidbody> ().freezeRotation = true;
		this.GetComponent<Rigidbody> ().angularDrag = 2.0f;
	}

	// Update is called once per frame
	void Update () {

	}


	void OnCollisionEnter (Collision hit)
	{
		if(hit.gameObject.tag == "floor")
		{
			isOnFloor = true;
		}

		transform.FindChild ("impact").GetComponent<AudioSource> ().pitch = Random.value + 0.25f;
		transform.FindChild ("impact").GetComponent<AudioSource> ().volume = Random.value * hit.relativeVelocity.magnitude *0.01f;

		transform.FindChild ("impact").GetComponent<AudioSource> ().Play ();

	}

	void OnCollisionExit (Collision hit)
	{
		if(hit.gameObject.tag == "floor")
		{
			isOnFloor = false;
		}
	}

    [Command]
    void CmdFire(Vector3 lookat)
    {

		Transform bulletSpawner = this.transform.FindChild ("bulletSpawner").transform;
        //print(bulletSpawner);

		GameObject newRocket = (GameObject)Instantiate(bulletPrefab, bulletSpawner.position, Quaternion.identity);
        newRocket.transform.LookAt(lookat);
        //newRocket.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        newRocket.GetComponent<Rigidbody>().velocity = newRocket.transform.forward * 220.0f;
        newRocket.GetComponent<Bullet>().owner = this.gameObject;
       // Physics.IgnoreCollision(newRocket.GetComponent<BoxCollider>(), GetComponent<MeshCollider>());

        // spawn the bullet on the clients
        NetworkServer.Spawn(newRocket);

        // when the bullet is destroyed on the server it will automaticaly be destroyed on clients
        Destroy(newRocket, 3.0f);
    }

	void FixedUpdate()
	{

        //gravity vector points at object from the center of the worldsphere
        Vector3 grav = this.transform.position;

        //multiple by the distance from the center
        float amount = grav.magnitude / 400.0f;
        amount = amount * amount;
        //amount *= 10.0f;

        this.GetComponent<Rigidbody>().AddForce(grav.normalized * amount*4.0f);

        //orient upwards

        Plane plane = new Plane(-grav.normalized, grav);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, plane.normal) * transform.rotation, 0.4f);

		if (!isLocalPlayer)
			return;

		//AIMING RAYCAST, SHOULD GET THIS FROM THE PLAYER INSTEAD OF RECALC HERE
		Ray camRay = GameObject.Find ("MainCam").GetComponent<Camera>().ScreenPointToRay (Input.mousePosition);

		Vector3 point = camRay.origin + camRay.direction * 10000.0f;
        Transform bulletSpawner = this.transform.FindChild("bulletSpawner").transform;

        Ray aimRay = new Ray(bulletSpawner.position, (point - bulletSpawner.position).normalized);
		Debug.DrawRay (aimRay.origin, aimRay.direction*100.0f, Color.blue);
		/////////////////////////////////////////////////////////////////////////

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
			if(isOnFloor)
				this.GetComponent<Rigidbody>().AddForce(-this.GetComponent<Rigidbody>().velocity.normalized*30.0f);
		}
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			this.GetComponent<Rigidbody>().AddForce(-transform.up*1000.0f);
		}

		if (Input.GetMouseButtonDown (0)) {
            CmdFire(aimRay.origin + aimRay.direction * 30.0f);
		}
		
		Debug.DrawRay (this.transform.position, this.transform.forward*100.0f, Color.red);
		Debug.DrawRay (this.transform.position, this.transform.up*100.0f, Color.yellow);
	}
}
