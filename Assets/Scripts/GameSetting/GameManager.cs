using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Varlab.Database.Domain;

public class GameManager : MonoBehaviour
{
    public enum GameLevel
    {
        LOBBY,
        INGAME,
        ENDSCREEN
    }
    public static GameManager instance = null;
    private BoardManager boardScript;
 //   private SoundManager soundScript;
    private EnemyManager enemyScript;
    private PlayerManager playScript;

    public bool IsGameStart;
    public bool IsAllPlayerDone;
    public GameLevel currentLevel;

    public List<int> connectedClientIDs;
    public List<Account> accounts;
    public int playersConnected;
    public int playersAlive;
    public int maximumEnemyLimit;
    public int remainingEnemies;
    public bool enableEnemySpawning;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            IsGameStart = false;
            IsAllPlayerDone = false;
            currentLevel = GameLevel.LOBBY;

            connectedClientIDs = new List<int>();
            playersConnected = 0;
            playersAlive = 0;
            //playScript.accounts = new List<Account>();

            enableEnemySpawning = false;
            if (maximumEnemyLimit <= 0) { maximumEnemyLimit = 5; }
            remainingEnemies = maximumEnemyLimit;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        boardScript = GetComponent<BoardManager>();
     //   soundScript = GetComponent<SoundManager>();
        enemyScript = GetComponent<EnemyManager>();
        playScript = GetComponent<PlayerManager>();

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


    }
    private void LateUpdate()
    {
        if (IsGameStart)
        {
            if (instance.playersAlive <= 0 && instance.currentLevel != GameLevel.ENDSCREEN)
            {
                instance.currentLevel = GameLevel.ENDSCREEN;
                NetworkManager.singleton.ServerChangeScene("EndScene");
            }

            if (instance.IsAllPlayerDone && instance.currentLevel != GameLevel.ENDSCREEN)
            {
                instance.IsAllPlayerDone = false;
                instance.currentLevel = GameLevel.ENDSCREEN;
                //NetworkManager.singleton.ServerChangeScene("EndScene");
            }
        }
    }

}


