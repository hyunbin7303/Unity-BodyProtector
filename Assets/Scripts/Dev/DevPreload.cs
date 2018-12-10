using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The DevPreload script starts everything from the "_preload" scene.
/// NOTE: DISABLE when in production...
/// </summary>
public class DevPreload : MonoBehaviour
{
    void Awake()
    {
        GameObject check = GameObject.Find("__app");
        if (check == null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("_preload");
        }
    }
}
