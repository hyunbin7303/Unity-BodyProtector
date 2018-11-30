using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResultManager : MonoBehaviour {

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            GameObject destroyplayer = GameObject.FindGameObjectWithTag("Player");
            Destroy(destroyplayer);
        }
    }


    public void RePlaygame()
    {
        Debug.Log("REPLAY GAME");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        Debug.Log("EXIT GAME PRESSED");

#else
        Application.Quit();
#endif
    }
}
