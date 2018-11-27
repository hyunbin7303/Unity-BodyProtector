using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InPlayGameManager : MonoBehaviour {

    private GameObject menuScreenPanel;
    private GameObject helpScreenPanel;
    private GameObject friendScreenPanel;

    private bool HelpScreenON;

    private void Start()
    {
        HelpScreenON = false;
        menuScreenPanel = GameObject.FindGameObjectWithTag("menuScreen");
        helpScreenPanel = GameObject.FindGameObjectWithTag("HelpScreen");
        friendScreenPanel = GameObject.FindGameObjectWithTag("FriendViewScreen");
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            CallMenuBar();
        }
    }
    public void CallMenuBar()
    {
        Debug.Log("Call Menu Bar");
        menuScreenPanel.transform.position = new Vector3(730f, 500f, 0f);
    }
    public void ContinueGame()
    {
        Debug.Log("Continue this game.");
        menuScreenPanel.transform.position = new Vector3(-500f, 0f, 0f);
    }
    public void HelpScreenDisplay()
    {
        if(!HelpScreenON)
        {
            Debug.Log("HelpScreen Display");
            helpScreenPanel.transform.position = new Vector3(730f, 500f, 0f);
            HelpScreenON = true;
        }
        else
        {
            Debug.Log("Help Screen Off");
            helpScreenPanel.transform.position = new Vector3(-500f, 0f, 0f);
            HelpScreenON = false;
        }
    }


    public void PauseGame()
    {
        Debug.Log("Pause Game");
        // networking.
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game button Pressed.");
        Application.Quit();
    }
    public void DisplayFriendInfo()
    {

        // Call Network Manager.
        //    NetworkManager.singleton.
    //    NetworkServer.connections;
    }
}
