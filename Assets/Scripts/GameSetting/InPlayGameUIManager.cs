using UnityEngine;

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
    }
    private void Update()
    {
     //   HealthNumber = mainPlayerInfo.
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Check Escape Button");
            CallMenuBar();
        }
        if(Input.GetKeyUp(KeyCode.F1))
        {
            Debug.Log("Check F1 Button");
            CallFriendbar();
        }
    }
    public void CallMenuBar()
    {
        menuScreenPanel = GameObject.FindGameObjectWithTag("menuScreen");
        if (!MainMenuScreenON)
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
        friendScreenPanel = GameObject.FindGameObjectWithTag("FriendViewScreen");
        if (!FriendScreenON)
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
        menuScreenPanel = GameObject.FindGameObjectWithTag("menuScreen");
        menuScreenPanel.transform.position = new Vector3(-500f, 0f, 0f);
    }
    public void HelpScreenDisplay()
    {
        helpScreenPanel = GameObject.FindGameObjectWithTag("HelpScreen");
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
        Debug.Log("Deug Display Game Info");
    }
}
