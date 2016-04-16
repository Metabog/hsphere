using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
public class HostButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Button> ().onClick.AddListener (() => { NetworkManager.singleton.StartHost();});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
