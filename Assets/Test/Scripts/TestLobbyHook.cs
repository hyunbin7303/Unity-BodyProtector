using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        Debug.Log("TestLobbyHook. OnLobbyServerSceneLoadedForPlayer. Apply our own settings to manager?");

        Debug.Log("TestLobbyHook. OnLobbyServerSceneLoadedForPlayer. PlayerController id: " + lobbyPlayer.GetComponent<NetworkIdentity>().playerControllerId);

        //gamePlayer.GetComponent<PlayerController>().myPlayerID = LobbyManager.s_Singleton.currentPlayerID.ToString();

        //base.OnLobbyServerSceneLoadedForPlayer(manager, lobbyPlayer, gamePlayer);
    }
}
