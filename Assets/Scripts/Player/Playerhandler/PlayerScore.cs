using System;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class PlayerScore : NetworkBehaviour
{
    [SyncVar(hook = "OnChangeScore")]
    public int currentScore = 0;
    public TMP_Text killCountText;


    private void Start()
    {
        killCountText.text = "0";
    }

    public void IncreaseScore(int amount)
    {
        if (!isServer)
            return;

        currentScore += amount;
    }
    void OnChangeScore(int score)
    {
        killCountText.text = score.ToString();
    }
}
