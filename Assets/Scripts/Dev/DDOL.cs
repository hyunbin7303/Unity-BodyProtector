using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The DDOL (Don't Destroy On Load) class is used to not 
/// destroy an essential GameObject that must persist scene to scene.
/// </summary>
public class DDOL : MonoBehaviour
{
    void Awake()
    {
        // Don't destroy the GameObject when loading
        // another scene or place...
        DontDestroyOnLoad(gameObject);
    }
}
