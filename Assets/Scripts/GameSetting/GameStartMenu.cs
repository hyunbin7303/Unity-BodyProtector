using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameStartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void HostStart()
    {
        Debug.Log("HOST Press Ready Pressed");

    }
    public void ClientPressReady()
    {
        Debug.Log("Client Press Ready Pressed");
       // NetworkManager.singleton.client client = new NetworkManager.singleton.client();

    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        Debug.Log("EXIT GAME PRESSED");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
