using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour {

	[SyncVar]
	public int team=-100;

	// Use this for initialization
	void Start () {
		team = -1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
