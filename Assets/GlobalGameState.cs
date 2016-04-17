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
		NetworkManager.singleton.StopHost();

	}

	public void setStateWin(int team)
	{
		winningTeam = team;
		gameState = kGlobalStateWin;

		GameObject.Find ("MainCam").GetComponent<Camera> ().enabled = false;

		if (winningTeam == BaseCoreScript.kTeamTypeBlue)
			GameObject.Find ("BlueCoreWincam").GetComponent<Camera> ().enabled = true;
		if (winningTeam == BaseCoreScript.kTeamTypeRed)
			GameObject.Find ("RedCoreWincam").GetComponent<Camera> ().enabled = true;

		QuitGameSoon (3.0f);
	}

	public void setStateGame()
	{
		winningTeam = -1;
		gameState = kGlobalStateGame;

		GameObject.Find ("RedCoreWincam").GetComponent<Camera> ().enabled = false;
		GameObject.Find ("BlueCoreWincam").GetComponent<Camera> ().enabled = false;
		GameObject.Find ("MainCam").GetComponent<Camera> ().enabled = true;
	}
}
