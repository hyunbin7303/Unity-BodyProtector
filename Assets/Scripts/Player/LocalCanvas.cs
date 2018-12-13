using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// For some ODD reason the UI overlap each other in multiplayer!?!
/// We use this script to ensure the our client-side can see THEIR OWN UI.
/// </summary>
/// <remarks>
/// I don't know if this is the right way to do it this way... Maybe there is sometype
/// of setting, script, or SOMETHING! How do professional game developers deal with this type of thing?
/// </remarks>
public class LocalCanvas : NetworkBehaviour
{
    void Start()
    {
        // First, if there are multiple canvas' within the player object...
        // then we must find the one with the "HUD" tag
        List<int> arrayIndexes = new List<int>();
        Canvas hudCanvas = null;
        var canvasComponents = GetComponentsInChildren<Canvas>();
        for (int i = 0; i < canvasComponents.Length; i++)
        {
            if (canvasComponents[i].CompareTag("HUD"))
            {
                // Remember the array index so that we can directly modify
                // the canvas that has the "HUD" tag
                arrayIndexes.Add(i);
                hudCanvas = canvasComponents[i];
            }
        }

        foreach (int arrayIndex in arrayIndexes)
        {
            // By default, disable the HUD Canvas if the developer forgets to disable in the Unity editor
            GetComponentsInChildren<Canvas>()[arrayIndex].enabled = false;

            if (isLocalPlayer)
            {
                DevLog.Log("LocalCanvas", "Enable UI for LocalPlayer netId: " + GetComponent<NetworkIdentity>().netId);
                // Enable the HUD UI canvas for our local player instance. This will ensure
                // that other player UIs will never overlap with the host or other clients...
                GetComponentsInChildren<Canvas>()[arrayIndex].enabled = true;
            }
        }
    }
}
