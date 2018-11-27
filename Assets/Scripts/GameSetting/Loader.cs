using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{

    public GameObject gameManager;          //GameManager prefab to instantiate.
    public GameObject databaseManager;

    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (GameManager.instance == null)
            //Instantiate gameManager prefab
            Instantiate(gameManager);
        else
            Debug.Log("[Loader.cs]: CallOrder(1) - GameManager instance already exists!");

        //Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
        //if (SoundManager.instance == null)

        //    //Instantiate SoundManager prefab
        //    Instantiate(soundManager);

        //Check if a DatabaseManager has already been assigned to static variable DatabaseManager.instance or if it's still null
        if (DatabaseManager.instance == null) {
            Instantiate(databaseManager);
        }
    }
}
