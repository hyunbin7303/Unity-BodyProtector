using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerScore : NetworkBehaviour
{
    public int currentScore = 0;
    public TMP_Text killCountText;

    private void Start()
    {
        killCountText.text = "0";
    }


    public void IncreaseScore(int amount)
    {
        if (!isServer)
        {
            Debug.Log("Is in the server?");
            return;
        }

        currentScore += amount;
        SetText();
    }

    public void SetText()
    {
        killCountText.text = currentScore.ToString();
    }
}
