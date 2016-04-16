using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class BaseCoreScript : NetworkBehaviour {

	int kTeamTypeRed = 0;
	int kTeamTypeBlue = 1;
	public int team;

	bool hasIssuedWarning = false;

	[SyncVar]
	int health = 80;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ReduceHealth()
	{
		health-=1;

		if (health == 0) {

			if(team == kTeamTypeBlue)
				GameObject.Find ("redWinrarSound").GetComponent<AudioSource>().Play();
			else if (team == kTeamTypeRed)
				GameObject.Find ("blueWinrarSound").GetComponent<AudioSource>().Play();


			health = 100;
			hasIssuedWarning = false;
			GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopHost();
		}

		if (health < 20 && !hasIssuedWarning) {
			hasIssuedWarning = true;

			if(team == kTeamTypeBlue)
				GameObject.Find ("blueCriticalSound").GetComponent<AudioSource>().Play();
			else if (team == kTeamTypeRed)
				GameObject.Find ("redCriticalSound").GetComponent<AudioSource>().Play();
		}

		string tag = "undefined";
		if (team == kTeamTypeBlue) {
			tag = "blueCoreHealthText";
			GameObject.Find ("blueTeamScoreText").GetComponent<Text>().text = "" +health;
		}
		if (team == kTeamTypeRed) {
			tag = "redCoreHealthText";
			GameObject.Find ("redTeamScoreText").GetComponent<Text>().text =  "" +health;
		}

		foreach(GameObject go in GameObject.FindGameObjectsWithTag(tag))
		{
			go.GetComponent<TextMesh>().text = "" + health;
		}
			

	}
}
