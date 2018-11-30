using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerScore : NetworkBehaviour
{
    public int currentScore = 0;
    public Text KillCountText;

    private void Start()
    {
        KillCountText.text = "0";
       // if(this.gameObject)
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
        KillCountText.text = currentScore.ToString();
    }
}
