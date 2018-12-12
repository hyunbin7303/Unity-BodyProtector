using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Messyspace;

public class Health : NetworkBehaviour
{
    [SyncVar(hook = "OnChangeHealth")]
    public float currentHealth = maxHealth;
    public const float maxHealth = 100f;

    public RectTransform healthBar;
    public Slider hudHealthBar;
    public TMP_Text healthText;

    /// The Player's stat includes the name, health, etc.
    private PlayerController thePlayer;


    void Start()
    {
        thePlayer = GetComponent<PlayerController>();
        if (isLocalPlayer)
        {
            if (hudHealthBar != null)
            {
                hudHealthBar.value = CalculateHealth();
            }
        }
    }

    /// <summary>
    /// Apply damage to the enemy.
    /// </summary>
    /// <param name="amount">The amount of damage to apply to the enemy.</param>
    public void EnemyTakeDamage(float amount, NetworkInstanceId attackerID)
    {
        if (!isServer)
            return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            GameObject player = NetworkServer.FindLocalObject(attackerID); //The server will find the player that is registered to the ID we have passed it.
            if (player != null)
            {
                player.GetComponent<PlayerStats>().IncreaseXP(10);
                player.GetComponent<PlayerScore>().IncreaseScore(1);
                Debug.Log("Player Experience : " + player.GetComponent<PlayerStats>().CurrentXP);
            }

            currentHealth = 0;
            Destroy(gameObject);
            Debug.Log("Enemy is Dead!");
        }
    }

    /// <summary>
    /// Apply damage to the player.
    /// </summary>
    /// <param name="amount">The amount of damage to apply to the player.</param>
    public void PlayerTakeDamage(float amount)
    {
        if (!isServer) {
            return;
        }

        //only take damage when the player is ALIVE
        if (thePlayer.status == CharacterStatus.ALIVE) {
            currentHealth -= amount;
        }

        if (currentHealth <= 0)
        {
            if (thePlayer.status == CharacterStatus.ALIVE)
            {
                // Once the player's health is zero, we call upon
                // all clients (and server) to sync data across network
                //CmdOnPlayerHealthZero();
                RpcOnPlayerHealthZero();
                OnPlayerHealthZero();
                currentHealth = 0;
            }
        }
    }

    void OnChangeHealth(float health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        if (hudHealthBar != null)
        {
            // apply changes to the health HUD
            hudHealthBar.value = health / maxHealth;
            healthText.text = health.ToString("F0");
        }
    }

    float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }

    
    /// <summary>
    /// RPC method when player health reaches zero.
    /// Tell all other clients that the player has zero health
    /// </summary>
    /// <remarks>
    /// The player is in the RESCUE state
    /// </remarks>
    [ClientRpc]
    void RpcOnPlayerHealthZero()
    {
        OnPlayerHealthZero();
    }

    /// <summary>
    /// Methods occurs when player's health reaches/is zero.
    /// </summary>
    void OnPlayerHealthZero()
    {
        DevLog.Log("Health", "The player health is zero... Changing color... syncing");

        // Change the color of the player to reflect their "dead" state
        GetComponentInChildren<MeshRenderer>().material.color = Color.red;

        // By setting the "isAlive" bool to false, we trigger
        // the SyncVar hook in the CharacterState
        thePlayer.status = CharacterStatus.RESCUE;
    }
}
