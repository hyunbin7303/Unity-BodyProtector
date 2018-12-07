using UnityEngine;

public class InPlayGameUIManager : MonoBehaviour
{
    public RectTransform menuScreenPanel;
    public RectTransform helpScreenPanel;
    public RectTransform friendScreenPanel;

    private Vector3 centerPosition = Vector3.zero;

    private GameObject mainPlayerInfo;
    private int HealthNumber;

    private bool MainMenuScreenON;
    private bool HelpScreenON;
    private bool FriendScreenON;


    private void Start()
    {
        menuScreenPanel.gameObject.SetActive(false);
        MainMenuScreenON = false;

        helpScreenPanel.gameObject.SetActive(false);
        HelpScreenON = false;

        friendScreenPanel.gameObject.SetActive(false);
        FriendScreenON = false;
    }


    private void Update()
    {
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
        Debug.Log("CallMenuBar method. Showing menu bar");
        if (menuScreenPanel.gameObject.activeSelf)
        {
            menuScreenPanel.localPosition = new Vector3(-2500f, 0f, 0f);
            menuScreenPanel.gameObject.SetActive(false);
        }
        else
        {
            menuScreenPanel.localPosition = centerPosition;
            menuScreenPanel.gameObject.SetActive(true);
        }
    }

    public void CallFriendbar()
    {
        if (friendScreenPanel.gameObject.activeSelf)
        {
            Debug.Log("Friend Screen Off");
            friendScreenPanel.localPosition = new Vector3(-2000f, 0f, 0f);
            friendScreenPanel.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Friend Screen On");
            friendScreenPanel.anchoredPosition3D = new Vector3(10, -400f, 0f);
            friendScreenPanel.gameObject.SetActive(true);
        }
    }

    public void HelpScreenDisplay()
    {
        if (helpScreenPanel.gameObject.activeSelf)
        {
            Debug.Log("Help Screen Off");
            helpScreenPanel.localPosition = new Vector3(-1500f, 0f, 0f);
            helpScreenPanel.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Help Screen On");
            helpScreenPanel.localPosition = centerPosition;
            helpScreenPanel.gameObject.SetActive(true);
        }
    }


    public void ContinueGame()
    {
        Debug.Log("Continue this game.");
        menuScreenPanel.gameObject.SetActive(false);
        helpScreenPanel.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        Debug.Log("Quit Game button Pressed.");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void DisplayFriendInfo()
    {

        // Call Network Manager.
        //    NetworkManager.singleton.
        //    NetworkServer.connections;
        Debug.Log("Deug Display Game Info");
    }
}
