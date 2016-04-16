using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ClientButtonScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GetComponent<Button> ().onClick.AddListener (() => { 
			NetworkManager.singleton.networkAddress = GameObject.Find ("ipField").GetComponentInChildren<Text> ().text;
			NetworkManager.singleton.networkPort = 7777;
			NetworkManager.singleton.StartClient ();
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
