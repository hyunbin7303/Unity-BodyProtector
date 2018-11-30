using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScrpt : MonoBehaviour {

    public void StartGame()
    {
        SceneManager.LoadScene("Lobby Scene");
    }

}
