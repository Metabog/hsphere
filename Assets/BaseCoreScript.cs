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
        // TODO update health text
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPlayerConnected(NetworkPlayer player)
    {
        //RpcUpdateHealthText();
    }

    [ClientRpc]
    void RpcUpdateHealthText(int new_health)
    {
        //update the displayed text
        string tag = "undefined";
        if (team == kTeamTypeBlue)
        {
            tag = "blueCoreHealthText";
            GameObject.Find("blueTeamScoreText").GetComponent<Text>().text = "" + new_health;
        }
        else if (team == kTeamTypeRed)
        {
            tag = "redCoreHealthText";
            GameObject.Find("redTeamScoreText").GetComponent<Text>().text = "" + new_health;
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag))
        {
            go.GetComponent<TextMesh>().text = "" + new_health;
        }
    }

    [ClientRpc]
    void RpcHitCoreEffects(int new_health)
    {
        // play sound effects
        if (new_health == 0)
        {
            if (team == kTeamTypeBlue)
                GameObject.Find("redWinrarSound").GetComponent<AudioSource>().Play();
            else if (team == kTeamTypeRed)
                GameObject.Find("blueWinrarSound").GetComponent<AudioSource>().Play();
        }
        else if (new_health < 20 && !hasIssuedWarning)
        {
            hasIssuedWarning = true;

            if (team == kTeamTypeBlue)
                GameObject.Find("blueCriticalSound").GetComponent<AudioSource>().Play();
            else if (team == kTeamTypeRed)
                GameObject.Find("redCriticalSound").GetComponent<AudioSource>().Play();
        }
    }

	public void ReduceHealth()
	{
        if (!isServer)
            return;

		health-=1;

        RpcHitCoreEffects(health);
        RpcUpdateHealthText(health);

        if (health == 0)
        {
            health = 100;
            hasIssuedWarning = false;
            GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopHost();
        }
	}
}
