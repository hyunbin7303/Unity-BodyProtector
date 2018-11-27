using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Varlab.Database.Domain;

public class PlayerManager : MonoBehaviour
{
    public int currentPlayerCount;
    public List<Account> accounts;


    public PlayerManager()
    {
        currentPlayerCount = 0;
        accounts = new List<Account>();
    }



    public void SetPlayerOffline(Account player)
    {
        for (int i = 0; i < GameManager.instance.playScript.accounts.Count; i++)
        {
            if (GameManager.instance.playScript.accounts[i].AccountID == player.AccountID)
            {
                GameManager.instance.playScript.accounts[i].IsOnline = false;
                break;
            }
        }
    }
}
