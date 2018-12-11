using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RescueTrigger : NetworkBehaviour
{
    public Button rescueButton;
    [SerializeField]
    private PlayerController playerToRescue;

    private string myNetId;
    private NetworkInstanceId targetId;


    //void Start()
    //{
    //    myNetId = gameObject.GetComponent<PlayerController>().tmpNetworkId;
    //    targetId = NetworkInstanceId.Invalid;

    //    if (isLocalPlayer)
    //    {
    //        rescueButton.onClick.AddListener(delegate { CmdRescueButtonOnClick(); });
    //        rescueButton.gameObject.SetActive(false);
    //    }
    //}

    //void OnDisable()
    //{
    //    DevLog.Log("ReviveTrigger", "OnDisable executing");
    //    rescueButton.onClick.RemoveListener(delegate { CmdRescueButtonOnClick(); });
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerToRescue = other.GetComponent<PlayerController>();
    //        // If the PLAYER i encounter is in a "RESCUE" state, therefore
    //        // it means that i can attempt to revive the other person...
    //        if (playerToRescue.characterState.status == CharacterStatus.RESCUE)
    //        {
    //            // DEBUG
    //            targetId = other.GetComponent<NetworkIdentity>().netId;

    //            // When PLAYER1 (myself) enters the revive zone, we want to show the revive ui box
    //            rescueButton.gameObject.SetActive(true);

    //            //DevLog.Log("ReviveTrigger", "Player id <" + myNetId + "> entered vicinity of <" + targetId.Value + ">");
    //        }
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        // If the PLAYER i encounter is in a "RESCUE" state, therefore
    //        // it means that i can attempt to revive the other person...
    //        if (playerToRescue != null && playerToRescue.characterState.status == CharacterStatus.RESCUE)
    //        {
    //            // DEBUG
    //            targetId = other.GetComponent<NetworkIdentity>().netId;

    //            // When PLAYER1 (myself) enters the revive zone, we want to show the revive ui box
    //            rescueButton.gameObject.SetActive(false);

    //            //DevLog.Log("ReviveTrigger", "Player id <" + myNetId + "> exited vicinity of <" + targetId.Value + ">");
    //        }
    //    }
    //}


    [Command]
    void CmdRescueButtonOnClick()
    {
        if (playerToRescue != null)
        {
            //DevLog.Log("ReviveTrigger", "Player id <" + myNetId + "> attempting to revive <" + this.playerToRescue.tmpNetworkId + ">");

            try
            {
                // Tell the server to RESCUE the downed player by restoring their health and their character appearance
                playerToRescue.CmdOnPlayerRescue();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                DevLog.Log("ReviveTrigger", "Exception thrown. Ignoring because player is already revived.\n" + error);
            }

            RpcSetRevivePanelActive(false);
        }
    }

    [ClientRpc]
    void RpcSetRevivePanelActive(bool isActive)
    {
        rescueButton.gameObject.SetActive(isActive);
    }
}
