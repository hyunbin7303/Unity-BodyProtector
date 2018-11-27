using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultManager : MonoBehaviour {


    public void RePlaygame()
    {
        Debug.Log("REPLAY GAME");
    }

    public void ExitGame()
    {
        Debug.Log("EXIT GAME PRESSED");
        Application.Quit();
    }

}
