using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Loads a Unity scene.
/// </summary>
public class LoadFirstScene : MonoBehaviour
{
    [Tooltip("Load a scene")]
    public string sceneName;

    void Awake()
    {
        GameObject check = GameObject.Find("__app");
        if (check != null)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                sceneName = "FirstScene";
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
