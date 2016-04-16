using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

using UnityEngine.UI;

public class Bogdan : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StartHosting()
	{
		print ("start hosting!");
		NetworkManager.singleton.StartHost ();
	}

	public void JoinGame()
	{
		NetworkManager.singleton.networkAddress = GameObject.Find ("ipField").GetComponentInChildren<Text> ().text;
		NetworkManager.singleton.networkPort = 7777;
		NetworkManager.singleton.StartClient ();
	}
}
