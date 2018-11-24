using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// TODO: comment
/// CustomNetworkManager class inherits from Unity's NetworkManager.
/// </summary>
public class CustomNetworkManager : NetworkManager
{
    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("OnPlayerConnected");
    }

    public override void OnStopServer()
    {
        Debug.Log("[NetworkManager]: Server is stopping. Host has stopped.");
        base.OnStopServer();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        Debug.Log("[NetworkManager]: Connection " + conn.connectionId + " has disconnected from the server.");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("[NetworkManager]: Connection " + conn.connectionId + " gained!");
    }


    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log("[NetworkManager]: Connection " + conn.connectionId + " lost!");
    }
}
