using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DevLog
{
    public static void Log(string className, string text)
    {
        UnityEngine.Debug.Log("[" + className + "] " + "JOHN CENA. " + text);
    }

    public static void Error(string className, string text)
    {
        UnityEngine.Debug.LogError("[" + className + "] " + "JOHN CENA. " + text);
    }
}
