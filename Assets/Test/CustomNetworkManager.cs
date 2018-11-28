using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Varlab.Database.Domain;

/// <summary>
/// TODO: comment
/// CustomNetworkManager class inherits from Unity's NetworkManager.
/// </summary>
public class CustomNetworkManager : NetworkManager
{



    public override void OnServerConnect(NetworkConnection conn)
    {
        GameManager.instance.IsGameStart = true;
        GameManager.instance.connectedClientIDs.Add(conn.connectionId);
        GameManager.instance.playersAlive += 1;
        GameManager.instance.playersConnected += 1;

        Debug.Log("[OnServerConnect] connection id " + conn.connectionId);
        base.OnServerConnect(conn);
    }

    public override void OnStopServer()
    {
        GameManager.instance.IsGameStart = false;
        Debug.Log("[NetworkManager]: Server is stopping. Host has stopped.");

        try
        {
            GameManager.instance.connectedClientIDs.Clear();
            GameManager.instance.playersAlive = 0;
            if (GameManager.instance.playersConnected > 0)
            {
                // We know that once server stops, no more players are connected to it,
                // even the host will not be connected
                GameManager.instance.playersConnected = 0;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("GameManager instance client id for HOST not found");
        }

        base.OnStopServer();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        GameManager.instance.connectedClientIDs.Remove(conn.connectionId);
        GameManager.instance.playersConnected -= 1;
        Debug.Log("[GameManager] amount of players left is " + GameManager.instance.playersConnected);

        base.OnServerDisconnect(conn);
        Debug.Log("[NetworkManager]: Connection " + conn.connectionId + " has disconnected from the server.");
    }

    //public override void OnClientConnect(NetworkConnection conn)
    //{
    //    //GameManager.instance.connectedClientIDs.Add(conn.connectionId);
    //    //GameManager.instance.playersConnected += 1;
    //    //Debug.Log("[GameManager] amount of players now " + GameManager.instance.playersConnected);

    //    //Account newPlayer = new Account();
    //    //newPlayer.AccountID = GameManager.instance.accounts.Count + 1;
    //    //newPlayer.Username = "KevAustin" + newPlayer.AccountID;
    //    //newPlayer.PasswordHash = "SOMETHING" + 100 + newPlayer.AccountID;
    //    //newPlayer.Email = newPlayer.Username + "@Conestogac.on.ca";

    //    //// Check if the Account exists in the database...
    //    //Account entity = DatabaseManager.instance.accountDAL.GetAccountByID(newPlayer.AccountID);
    //    //if (entity == null)
    //    //{
    //    //    // If the account does not exist, this means that a new player record is inserted
    //    //    DatabaseManager.instance.accountDAL.CreateAccount(newPlayer);
    //    //}
    //    //else
    //    //{
    //    //    // Otherwise, we found the account information... save it to this local var
    //    //    newPlayer = entity;
    //    //}

    //    //// Add to the list of accounts...
    //    //GameManager.instance.accounts.Add(newPlayer);

    //    base.OnClientConnect(conn);
    //    Debug.Log("[NetworkManager]: Connection " + conn.connectionId + " gained!");
    //}


    //public override void OnClientDisconnect(NetworkConnection conn)
    //{
    //    //GameManager.instance.connectedClientIDs.Remove(conn.connectionId);
    //    //GameManager.instance.playersConnected -= 1;
    //    //Debug.Log("[GameManager] amount of players left is " + GameManager.instance.playersConnected);

    //    base.OnClientDisconnect(conn);
    //    Debug.Log("[NetworkManager]: Connection " + conn.connectionId + " lost!");
    //}


    public void StartUpHost()
    {
        Debug.Log("StartUpHost Pressed");
        setPort();
        singleton.StartHost();
    }
    public void JoinGame()
    {
        setIPAddress();
        setPort();
        NetworkManager.singleton.StartClient();
    }
    void setPort()
    {
        singleton.networkPort = 7777;
    }
    void setIPAddress()
    {
        string ipAddress = GameObject.FindGameObjectWithTag("IPAddressTag").GetComponent<Text>().text;
        Debug.Log("IPADDRESS INPUT : " + ipAddress);
        singleton.networkAddress = ipAddress;
    }
    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }


    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
