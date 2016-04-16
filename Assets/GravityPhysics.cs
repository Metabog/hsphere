using UnityEngine;
using System.Collections;

public class GravityPhysics : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		//gravity vector points at object from the center of the worldsphere
		Vector3 grav = this.transform.position;
		
		//multiple by the distance from the center
		float amount = grav.magnitude/400.0f;
		amount = amount * amount;
		//amount *= 10.0f;
		
		this.GetComponent<Rigidbody>().AddForce(grav.normalized*amount);
	}
}
