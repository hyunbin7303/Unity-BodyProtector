using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class InPlayGameUIManager : MonoBehaviour
{
    private GameObject menuScreenPanel;
    private GameObject helpScreenPanel;
    private GameObject friendScreenPanel;

    private GameObject mainPlayerInfo;
    private int HealthNumber;

    private bool MainMenuScreenON;
    private bool HelpScreenON;
    private bool FriendScreenON;

    private void Start()
    {
        MainMenuScreenON = false;
        HelpScreenON = false;
        FriendScreenON = false;
        menuScreenPanel = GameObject.FindGameObjectWithTag("menuScreen");
        helpScreenPanel = GameObject.FindGameObjectWithTag("HelpScreen");
        friendScreenPanel = GameObject.FindGameObjectWithTag("FriendViewScreen");
    }
    private void Update()
    {
     //   HealthNumber = mainPlayerInfo.
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CallMenuBar();
        }
    }
    public void CallMenuBar()
    {
        if(!MainMenuScreenON)
        {
            menuScreenPanel.transform.position = new Vector3(730f, 500f, 0f);
            MainMenuScreenON = true;
        }
        else
        {
            menuScreenPanel.transform.position = new Vector3(-500f, 0f, 0f);
            MainMenuScreenON = false;

        }
    }
    public void CallFriendbar()
    {
        if(!FriendScreenON)
        {
            friendScreenPanel.transform.position = new Vector3(730f, 500f, 0f);
            FriendScreenON = true;
        }
        else
        {
            friendScreenPanel.transform.position = new Vector3(-500f, 0f, 0f);
            FriendScreenON = false;
        }
    }


    public void ContinueGame()
    {
        Debug.Log("Continue this game.");
        menuScreenPanel.transform.position = new Vector3(-500f, 0f, 0f);
    }
    public void HelpScreenDisplay()
    {
        if (!HelpScreenON)
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
