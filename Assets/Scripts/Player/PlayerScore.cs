using System;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
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
        if (!isLocalPlayer)
            return;

        currentScore += amount;
        Debug.Log("JOHN CENA. Player score: " + currentScore);
        SetText();
    }

    public void SetText()
    {
        killCountText.text = currentScore.ToString();
    }
}
