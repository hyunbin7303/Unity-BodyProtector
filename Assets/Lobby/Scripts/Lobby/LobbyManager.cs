using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Prototype.NetworkLobby
{
    public class LobbyManager : NetworkLobbyManager 
    {
        private const string kDefaultHost = "localhost";
        static short MsgKicked = MsgType.Highest + 1;

        static public LobbyManager s_Singleton;


        [Header("Unity UI Lobby")]
        [Tooltip("Time in second between all players ready & match start")]
        public float prematchCountdown = 5.0f;

        [Space]
        [Header("UI Reference")]
        public LobbyTopPanel topPanel;

        public RectTransform mainMenuPanel;
        public RectTransform lobbyPanel;

        public LobbyInfoPanel infoPanel;
        public LobbyCountdownPanel countdownPanel;
        public GameObject addPlayerButton;

        protected RectTransform currentPanel;

        public Button backButton;

        public Text statusInfo;
        public Text hostInfo;

        //Client numPlayers from NetworkManager is always 0, so we count (throught connect/destroy in LobbyPlayer) the number
        //of players, so that even client know how many player there is.
        [HideInInspector]
        public int _playerNumber = 0;

        //used to disconnect a client properly when exiting the matchmaker
        [HideInInspector]
        public bool _isMatchmaking = false;

        protected bool _disconnectServer = false;
        
        protected ulong _currentMatchID;

        protected LobbyHook _lobbyHooks;

        // This region is for our Game Manager properties that exists
        // when the scene is active (i.e. server is started and players in online scene)
        #region Game Manager Properties
        [Header("Game Manager Properties")]
        public GameState gameState;

        //Keeps tracks of how many players have connected/joined a lobby
        //it is also used for uniquely identifying the players in the active scene
        public int currentPlayerID = 0;

        // Tracks how many players are alive in the online scene
        public int playersAlive;

        // How many enemies may spawn in the online scene
        public int maxEnemyLimit;
        // The remaining number of enemies in the scene
        public int remainingEnemies;

        public List<PlayerInfoStruct> playerInfoList = new List<PlayerInfoStruct>();

        public class PlayerInfoStruct
        {
            public string playerID;
            public string playerState;
        }
        #endregion

        void Start()
        {
            s_Singleton = this;
            _lobbyHooks = GetComponent<Prototype.NetworkLobby.LobbyHook>();
            currentPanel = mainMenuPanel;

            backButton.gameObject.SetActive(false);
            GetComponent<Canvas>().enabled = true;

            DontDestroyOnLoad(gameObject);

            SetServerInfo("Offline", "None");

            InitializeGameManagerProperties();
        }

        void InitializeGameManagerProperties()
        {
            gameState = GameState.INACTIVE;
            playersAlive = 0;

            if (maxEnemyLimit <= 0) { maxEnemyLimit = 5; }
            remainingEnemies = maxEnemyLimit;
        }

        public override void OnLobbyClientSceneChanged(NetworkConnection conn)
        {
            if (SceneManager.GetSceneAt(0).name == lobbyScene)
            {
                if (topPanel.isInGame)
                {
                    ChangeTo(lobbyPanel);
                    if (_isMatchmaking)
                    {
                        if (conn.playerControllers[0].unetView.isServer)
                        {
                            backDelegate = StopHostClbk;
                        }
                        else
                        {
                            backDelegate = StopClientClbk;
                        }
                    }
                    else
                    {
                        if (conn.playerControllers[0].unetView.isClient)
                        {
                            backDelegate = StopHostClbk;
                        }
                        else
                        {
                            backDelegate = StopClientClbk;
                        }
                    }
                }
                else
                {
                    ChangeTo(mainMenuPanel);
                }

                topPanel.ToggleVisibility(true);
                topPanel.isInGame = false;
            }
            else
            {
                ChangeTo(null);

                Destroy(GameObject.Find("MainMenuUI(Clone)"));

                //backDelegate = StopGameClbk;
                topPanel.isInGame = true;
                topPanel.ToggleVisibility(false);
            }
        }

        public void ChangeTo(RectTransform newPanel)
        {
            if (currentPanel != null)
            {
                currentPanel.gameObject.SetActive(false);
            }

            if (newPanel != null)
            {
                newPanel.gameObject.SetActive(true);
            }

            currentPanel = newPanel;

            if (currentPanel != mainMenuPanel)
            {
                backButton.gameObject.SetActive(true);
            }
            else
            {
                backButton.gameObject.SetActive(false);
                SetServerInfo("Offline", "None");
                _isMatchmaking = false;
            }
        }

        public void DisplayIsConnecting()
        {
            var _this = this;
            infoPanel.Display("Connecting...", "Cancel", () => { _this.backDelegate(); });
        }

        public void SetServerInfo(string status, string host)
        {
            statusInfo.text = status;
            hostInfo.text = host;
        }


        public delegate void BackButtonDelegate();
        public BackButtonDelegate backDelegate;
        public void GoBackButton()
        {
            backDelegate();
			topPanel.isInGame = false;
        }

        // ----------------- Server management

        public void AddLocalPlayer()
        {
            TryToAddPlayer();
        }

        public void RemovePlayer(LobbyPlayer player)
        {
            player.RemovePlayer();
        }

        public void SimpleBackClbk()
        {
            ChangeTo(mainMenuPanel);
        }
                 
        public void StopHostClbk()
        {
            if (_isMatchmaking)
            {
				matchMaker.DestroyMatch((NetworkID)_currentMatchID, 0, OnDestroyMatch);
				_disconnectServer = true;
            }
            else
            {
                StopHost();
            }

            // Once a match is completed or host has decided to exit,
            // set our gamestate back to INACTIVE
            InitializeGameManagerProperties();

            ChangeTo(mainMenuPanel);
        }

        public void StopClientClbk()
        {
            StopClient();

            if (_isMatchmaking)
            {
                StopMatchMaker();
            }

            ChangeTo(mainMenuPanel);
        }

        public void StopServerClbk()
        {
            StopServer();
            // Once a match is completed or host has decided to exit,
            // set our gamestate back to INACTIVE
            InitializeGameManagerProperties();
            ChangeTo(mainMenuPanel);
        }

        class KickMsg : MessageBase { }
        public void KickPlayer(NetworkConnection conn)
        {
            conn.Send(MsgKicked, new KickMsg());
        }

        public void KickedMessageHandler(NetworkMessage netMsg)
        {
            infoPanel.Display("Kicked by Server", "Close", null);
            netMsg.conn.Disconnect();
        }

        //===================

        public override void OnStartHost()
        {
            base.OnStartHost();

            ChangeTo(lobbyPanel);
            backDelegate = StopHostClbk;

            SetServerInfo("Hosting", GetIPAddress());
        }

		public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
		{
			base.OnMatchCreate(success, extendedInfo, matchInfo);
            _currentMatchID = (System.UInt64)matchInfo.networkId;
		}

		public override void OnDestroyMatch(bool success, string extendedInfo)
		{
			base.OnDestroyMatch(success, extendedInfo);
			if (_disconnectServer)
            {
                StopMatchMaker();
                StopHost();
            }
        }

        //allow to handle the (+) button to add/remove player
        public void OnPlayersNumberModified(int count)
        {
            Debug.Log("LobbyManager. OnPlayersNumberModified. Modifying PlayerNumbers by: " + count);
            _playerNumber += count;

            int localPlayerCount = 0;
            foreach (UnityEngine.Networking.PlayerController p in ClientScene.localPlayers)
            {
                localPlayerCount += (p == null || p.playerControllerId == -1) ? 0 : 1;
            }

            addPlayerButton.SetActive(localPlayerCount < maxPlayersPerConnection && _playerNumber < maxPlayers);
        }

        // ----------------- Server callbacks ------------------

        //we want to disable the button JOIN if we don't have enough player
        //But OnLobbyClientConnect isn't called on hosting player. So we override the lobbyPlayer creation
        public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
        {
            Debug.Log("LobbyManager. OnLobbyServerCreateLobbPlayer. Creating LobbyPlayer with connection id: " + conn.connectionId + " and " + playerControllerId);
            GameObject obj = Instantiate(lobbyPlayerPrefab.gameObject) as GameObject;

            LobbyPlayer newPlayer = obj.GetComponent<LobbyPlayer>();
            newPlayer.ToggleJoinButton(numPlayers + 1 >= minPlayers);

            OnPlayersNumberModified(1);

            // Anytime a player joins the lobby, we want to increase the client connection count
            // always by 1. We use this variable to assign a unique ID to the lobby player/player.
            currentPlayerID += 1;
            obj.GetComponent<LobbyPlayer>().lobbyPlayerID = currentPlayerID.ToString();
            playerInfoList.Add(new PlayerInfoStruct { playerID = currentPlayerID.ToString(), playerState = "ALIVE" });

            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                LobbyPlayer p = lobbySlots[i] as LobbyPlayer;

                if (p != null)
                {
                    p.RpcUpdateRemoveButton();
                    p.ToggleJoinButton(numPlayers + 1 >= minPlayers);
                }
            }

            return obj;
        }

        public override void OnLobbyServerPlayerRemoved(NetworkConnection conn, short playerControllerId)
        {
            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                LobbyPlayer p = lobbySlots[i] as LobbyPlayer;

                if (p != null)
                {
                    p.RpcUpdateRemoveButton();
                    p.ToggleJoinButton(numPlayers + 1 >= minPlayers);
                }
            }
        }

        public override void OnLobbyServerDisconnect(NetworkConnection conn)
        {
            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                LobbyPlayer p = lobbySlots[i] as LobbyPlayer;

                if (p != null)
                {
                    p.RpcUpdateRemoveButton();
                    p.ToggleJoinButton(numPlayers >= minPlayers);
                }
            }
        }

        public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
        {
            //This hook allows you to apply state data from the lobby-player to the game-player
            //just subclass "LobbyHook" and add it to the lobby object.
            var lobbyPlayerId = lobbyPlayer.GetComponent<NetworkIdentity>().netId.ToString();
            var gamePlayerId = lobbyPlayer.GetComponent<NetworkIdentity>().netId.ToString();

            //We assign the lobbyPlayerID to the playerID of the gameObject
            //This is so we can track the player from in the manager and the game
            gamePlayer.GetComponent<PlayerController>().myPlayerID = lobbyPlayer.GetComponent<LobbyPlayer>().lobbyPlayerID;

            Debug.Log("LobbyManager. OnLobbyServerSceneLoadedForPlayer. netId lobbyPlayer: " + lobbyPlayerId + " and netId gamePlayer: " + gamePlayerId);

            if (_lobbyHooks)
                _lobbyHooks.OnLobbyServerSceneLoadedForPlayer(this, lobbyPlayer, gamePlayer);

            return true;
        }

        // --- Countdown management

        public override void OnLobbyServerPlayersReady()
        {
            bool allReady = true;
			for(int i = 0; i < lobbySlots.Length; ++i)
			{
				if(lobbySlots[i] != null)
					allReady &= lobbySlots[i].readyToBegin;
			}

            // when all players are ready, start the match
            if (allReady)
            {
                StartCoroutine(ServerCountdownCoroutine());                
            }
        }

        public IEnumerator ServerCountdownCoroutine()
        {
            gameState = GameState.MATCH_STARTING;
            float remainingTime = prematchCountdown;
            int floorTime = Mathf.FloorToInt(remainingTime);

            while (remainingTime > 0)
            {
                yield return null;

                remainingTime -= Time.deltaTime;
                int newFloorTime = Mathf.FloorToInt(remainingTime);

                if (newFloorTime != floorTime)
                {//to avoid flooding the network of message, we only send a notice to client when the number of plain seconds change.
                    floorTime = newFloorTime;

                    for (int i = 0; i < lobbySlots.Length; ++i)
                    {
                        if (lobbySlots[i] != null)
                        {//there is maxPlayer slots, so some could be == null, need to test it before accessing!
                            (lobbySlots[i] as LobbyPlayer).RpcUpdateCountdown(floorTime);
                        }
                    }
                }
            }

            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                if (lobbySlots[i] != null)
                {
                    (lobbySlots[i] as LobbyPlayer).RpcUpdateCountdown(0);
                }
            }


            gameState = GameState.INGAME;   // this means the match has started
            playersAlive = _playerNumber;   // set number of playersAlive to players in the match
            ServerChangeScene(playScene);   // This changes the scene to the active network scene
        }

        // ----------------- Client callbacks ------------------

        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);

            infoPanel.gameObject.SetActive(false);

            conn.RegisterHandler(MsgKicked, KickedMessageHandler);

            if (!NetworkServer.active)
            {//only to do on pure client (not self hosting client)
                ChangeTo(lobbyPanel);
                backDelegate = StopClientClbk;
                SetServerInfo("Client", networkAddress);
            }
        }


        public override void OnClientDisconnect(NetworkConnection conn)
        {
            base.OnClientDisconnect(conn);
            Debug.Log("LobbyManager. OnClientDisconnect. Client id <" + conn.connectionId + "> has disconnected from game");
            ChangeTo(mainMenuPanel);
        }

        public override void OnClientError(NetworkConnection conn, int errorCode)
        {
            ChangeTo(mainMenuPanel);
            infoPanel.Display("Client error : " + (errorCode == 6 ? "timeout" : errorCode.ToString()), "Close", null);
        }


        // ----------------- Our Custom functions ------------------


        /// <summary>
        /// This is where our main NetworkManager game-loop is located. It decides
        /// the game state and how to proceed...
        /// </summary>
        void Update()
        {
            if (gameState == GameState.INGAME)
            {
                if (remainingEnemies <= 0)
                {
                    Debug.Log("LobbyManager. NO MORE ENEMIES REMAINING!");
                    gameState = GameState.WIN_GAME;
                }
                if (playersAlive <= 0)
                {
                    Debug.Log("LobbyManager. NO MORE PLAYERS ALIVE!");
                    gameState = GameState.LOSE_GAME;
                }
            }

            // The Players have won the game by destroying all the enemies...
            if (gameState == GameState.WIN_GAME)
            {
                OnGameWon();
            }
            // The Players have lost because they have all died...
            if (gameState == GameState.LOSE_GAME)
            {
                OnGameIsLost();
            }
        }

        public void ChangePlayerState(string playerID, string newState)
        {
            for (int i = 0; i < playerInfoList.Count; i++)
            {
                var playerInfo = playerInfoList[i];
                if (playerID == playerInfo.playerID)
                {
                    // Found the playerID... reflect their state in the list
                    playerInfoList[i].playerState = newState;
                    break;
                }
            }
        }

        private PlayerInfoStruct GetPlayerInfoStruct(string playerID)
        {
            for (int i = 0; i < playerInfoList.Count; i++)
            {
                var playerInfo = playerInfoList[i];

                if (playerID == playerInfo.playerID)
                {
                    return playerInfo;
                }
            }
            return new PlayerInfoStruct();
        }

        /// <summary>
        /// When the player joins is alive
        /// </summary>
        public void AddPlayerAlive()
        {
            playersAlive++;
        }

        public void DecreasePlayerAlive(string playerID, int count)
        {
            playersAlive += count;
            if (playersAlive <= 0)
            {
                playersAlive = 0;
            }

            // TEST
            PlayerInfoStruct playerInfo = GetPlayerInfoStruct(playerID);
            if (!string.IsNullOrEmpty(playerInfo.playerID))
            {
                Debug.Log("LobbyManager. DecreasePlayerAlive. Found player info id <" + playerID + "> with state of <" + playerInfo.playerState + ">");

                if (playerInfo.playerState != "DEAD")
                {
                }
                else
                {
                }
            }
            else
            {
                Debug.LogError("LobbyManager. DecreasePlayerAlive. Did not find the player info id <" + playerID + ">");
            }
        }

        //handle the adding and removing of enemies
        public void OnEnemyNumberModified(int count)
        {
            remainingEnemies += count;
        }


        void OnGameWon()
        {
            gameState = GameState.INACTIVE;
            ServerChangeScene("EndScene");
        }

        void OnGameIsLost()
        {
            gameState = GameState.INACTIVE;
            ServerChangeScene("EndScene");   // This changes the scene to the active network scene
        }

        // ----------------- Utility functions ------------------

        public string GetIPAddress()
        {
            networkAddress = IPManager.GetIP(ADDRESSFAM.IPv4);
            if (string.IsNullOrEmpty(networkAddress))
                networkAddress = kDefaultHost;
            return networkAddress;
        }
    }
}

/// <summary>
/// The current state of the game.
/// Basically a state machine for the states of the game.
/// </summary>
public enum GameState
{
    INACTIVE,
    DEDICATED_SERVER,
    MATCH_STARTING,
    INGAME,
    WIN_GAME,
    LOSE_GAME
}