using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameStartMenu : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }


    public void StartServer()
    {
        SceneManager.LoadScene("Lobby Scene");
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
