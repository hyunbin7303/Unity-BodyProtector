using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameStartMenu : CustomNetworkManager {

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
        Application.Quit();
    }
}
