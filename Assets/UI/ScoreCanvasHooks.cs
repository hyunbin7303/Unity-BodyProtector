using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class ScoreCanvasHooks : MonoBehaviour
{
    public delegate void CanvasHook();

    public CanvasHook OnAllPlayersDie;

    public Text killCount;


    public void SetCount(int count)
    {
        killCount.text = count.ToString();
    }
}
