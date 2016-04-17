using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GlobalGameState : MonoBehaviour {

	static int kGlobalStateGame = 0; 
	static int kGlobalStateWin = 1;

	static int winningTeam = -1;
	static int gameState = 0;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	
	IEnumerator QuitGameSoon(float time)
	{
		yield return new WaitForSeconds(time);
		
		// Code to execute after the delay
		NetworkManager.singleton.StopHost ();
		NetworkManager.singleton.StopClient ();
		print ("QUIT TO MENU!");
	}

	public void setStateWin(int team)
	{
		winningTeam = team;
		gameState = kGlobalStateWin;

		GameObject.Find ("MainCam").GetComponent<Camera> ().enabled = false;
		GameObject.Find ("MainCam").GetComponent<AudioListener> ().enabled = false;

		if (winningTeam == BaseCoreScript.kTeamTypeBlue)
		{
			GameObject.Find ("RedCoreWincam").GetComponent<Camera>().enabled=true;
			GameObject.Find ("RedCoreWincam").GetComponent<AudioListener>().enabled=true;
		}
		if (winningTeam == BaseCoreScript.kTeamTypeRed)
		{
			GameObject.Find ("BlueCoreWincam").GetComponent<Camera>().enabled=true;
			GameObject.Find ("BlueCoreWincam").GetComponent<AudioListener>().enabled=true;
		}

		QuitGameSoon (3.0f);
	}

	public void setStateGame()
	{
		winningTeam = -1;
		gameState = kGlobalStateGame;

		GameObject.Find ("RedCoreWincam").GetComponent<Camera>().enabled=false;
		GameObject.Find ("BlueCoreWincam").GetComponent<Camera>().enabled=false;
		GameObject.Find ("MainCam").GetComponent<Camera>().enabled=true;

		GameObject.Find ("RedCoreWincam").GetComponent<AudioListener>().enabled=false;
		GameObject.Find ("BlueCoreWincam").GetComponent<AudioListener>().enabled=false;
		GameObject.Find ("MainCam").GetComponent<AudioListener>().enabled=true;
	}
}
