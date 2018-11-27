using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Varlab.Database.Domain;

public class PlayerManager : MonoBehaviour
{
    public List<Account> accounts;

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
