using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResultManager : MonoBehaviour
{
    public Button backToLobbyButton;



    void OnEnable()
    {
        if (LobbyManager.s_Singleton != null)
        {
            backToLobbyButton.onClick.AddListener(LobbyManager.s_Singleton.GoBackButton);
        }
    }

    void OnDisable()
    {
        backToLobbyButton.onClick.RemoveAllListeners();
    }

    void Update()
    {
        //if (GameObject.FindGameObjectWithTag("Player"))
        //{
        //    GameObject destroyplayer = GameObject.FindGameObjectWithTag("Player");
        //    Destroy(destroyplayer);
        //}
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        Debug.Log("GameResultManager. ExitGame. EXIT GAME PRESSED");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
