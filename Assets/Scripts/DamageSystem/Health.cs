using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Health : NetworkBehaviour
{
    [SyncVar(hook = "OnChangeHealth")]
    public float currentHealth = maxHealth;
    public const float maxHealth = 100f;

    public RectTransform healthBar;
    public Slider hudHealthBar;
    public TMP_Text healthText;

    public void TakeDamage(float amount, NetworkInstanceId attackerID)
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

    public void PlayerTakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        if (hudHealthBar != null)
        {
            // apply changes to the health HUD
            hudHealthBar.value = CalculateHealth();
            healthText.text = currentHealth.ToString("F0");
        }
    }

    // Use this for initialization
    void Start()
    {
        if (hudHealthBar != null)
        {
            hudHealthBar.value = CalculateHealth();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnChangeHealth(float health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
    }

    float CalculateHealth()
    {
        return currentHealth / maxHealth;
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
