using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ClientButtonScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GetComponent<Button> ().onClick.AddListener (() => {
            string addr = GameObject.Find("ipField").GetComponentInChildren<Text>().text;
            if (addr == "enter host ip")
                addr = "localhost";
            NetworkManager.singleton.networkAddress = addr;   
			NetworkManager.singleton.networkPort = 7777;
			NetworkManager.singleton.StartClient ();
		});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
