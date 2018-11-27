using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Varlab.DataLayer;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager instance = null;

    public AccountDAL accountDAL = null;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            accountDAL = new AccountDAL();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
