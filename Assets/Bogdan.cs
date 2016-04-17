using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

using UnityEngine.UI;

public class Bogdan : NetworkManager {

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


	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		GameObject player = (GameObject)Instantiate(Resources.Load ("PlayerCapsule"), Vector3.zero, Quaternion.identity);
	
		//player.GetComponent<Player>().color = Color.Red;
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

		int nPlayersInBlue = 0;
		int nPlayersInRed = 0;

		foreach (GameObject pl in  GameObject.FindGameObjectsWithTag("PLAYER")) {
			if(pl.GetComponent<PlayerData>().team == BaseCoreScript.kTeamTypeBlue)
				nPlayersInBlue++;
			if(pl.GetComponent<PlayerData>().team == BaseCoreScript.kTeamTypeRed)
				nPlayersInRed++;
		}

		//randomly assign first
		int team = Random.Range (0, 2);

		print ("nblue " + nPlayersInBlue + " nred " + nPlayersInRed);

		//then choose the team with least players if they are not equal
		if (nPlayersInRed > nPlayersInBlue)
			team = BaseCoreScript.kTeamTypeBlue;
		else if(nPlayersInRed < nPlayersInBlue)
			team = BaseCoreScript.kTeamTypeRed;

		player.GetComponent<PlayerData> ().team = team;

		if (team == BaseCoreScript.kTeamTypeRed) {
			print("Players spawned on red");
			player.transform.FindChild ("skateman").FindChild ("default").GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.red);
			player.transform.FindChild ("playerLight").gameObject.GetComponent<Light>().color = Color.red;
			player.transform.position = GameObject.FindGameObjectsWithTag ("redSpawn") [Random.Range(0,4)].transform.position;

		} else if (team == BaseCoreScript.kTeamTypeBlue) {
			player.transform.FindChild ("skateman").FindChild ("default").GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.blue);
			player.transform.FindChild ("playerLight").gameObject.GetComponent<Light>().color = Color.blue;
			player.transform.position = GameObject.FindGameObjectsWithTag ("blueSpawn") [Random.Range(0,4)].transform.position;
			print("Players spawned on blue");

		}
		player.GetComponent<PlayerData>().team = team;



	}
}
