using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Health : NetworkBehaviour
{
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    public RectTransform healthBar;

    public const int maxHealth = 100;
    public void TakeDamage(int amount, NetworkInstanceId attackerID)
    {
        if (!isServer)
        {
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            GameObject player = NetworkServer.FindLocalObject(attackerID); //The server will find the player that is registered to the ID we have passed it.
            if (player != null)
            {
                Debug.Log("Found player object");
                player.GetComponent<PlayerScore>().IncreaseScore(1);
            }
            else
            {
                Debug.Log("Did not find player object");
            }

            currentHealth = 0;
            Debug.Log("Dead!");
        }
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnChangeHealth(int health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }
    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // move back to zero location
            transform.position = Vector3.zero;
        }
    }
}
