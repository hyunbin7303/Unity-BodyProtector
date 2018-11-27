using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameLevel
    {
        LOBBY,
        INGAME,
        ENDSCREEN
    }

    public static GameManager instance = null;
    public BoardManager boardScript;
    public SoundManager soundScript;
    public EnemyManager enemyScript;
    public PlayerManager playScript;
    public bool IsGameStart;
    public bool IsAllPlayerDone;

    public GameLevel currentLevel;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            IsGameStart = false;
            IsAllPlayerDone = false;
            currentLevel = GameLevel.LOBBY;
            playScript.accounts = new List<Varlab.Database.Domain.Account>();

            Debug.Log("[Loader.cs]: CallOrder(2) - Created the GameManager instance");
        }
        else if (instance != this)
        {
            Debug.Log("[Loader.cs]: CallOrder(3) - Destroying the GameManager!");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    public void setGamePlayerDone(bool OnOFF)
    {
        IsAllPlayerDone = OnOFF;
    }
    void InitGame()
    {
    }
    // Update is called once per frame
    void Update()
    {
        // if the game is done, go to final page.
        // If Both of player die, it goes to the end scene.
        //if (IsGameStart)
        //{
        //    FinishGame();
        //}
        //// Check whether all players are gone.
        //if (IsAllPlayerDone)
        //{
        //    NetworkManager.singleton.ServerChangeScene("EndScene");
        //}


    }
    private void LateUpdate()
    {
        if (IsGameStart)
        {
            if (playScript != null)
            {
                if (playScript.accounts != null)
                {
                    var count = playScript.accounts.Count(x => x.IsOnline);
                    if (count == 0)
                    {
                        IsAllPlayerDone = true;
                    }
                }
            }
            if (instance.IsAllPlayerDone && instance.currentLevel != GameLevel.ENDSCREEN)
            {
                instance.IsAllPlayerDone = false;
                instance.currentLevel = GameLevel.ENDSCREEN;
                NetworkManager.singleton.ServerChangeScene("EndScene");
            }
        }
    }

}


