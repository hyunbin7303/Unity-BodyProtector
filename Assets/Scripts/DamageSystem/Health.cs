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


    void Start()
    {
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
    public void EnemyTakeDamage(float amount/*, NetworkInstanceId attackerID*/)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            //GameObject player = NetworkServer.FindLocalObject(attackerID); //The server will find the player that is registered to the ID we have passed it.
            //if (player != null)
            //{
            //    Debug.Log("Found player object");
            //    player.GetComponent<PlayerScore>().IncreaseScore(1);
            //}
            //else
            //{
            //    Debug.Log("Did not find player object");
            //}

            currentHealth = 0;
            Destroy(gameObject);
            Debug.Log("Enemy is Dead!");
        }
        else
        {
            healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
            if (hudHealthBar != null)
            {
                Debug.Log("NOT IN HERE!1");
                // apply changes to the health HUD
                hudHealthBar.value = CalculateHealth();
                healthText.text = currentHealth.ToString("F0");
            }
        }
    }

    /// <summary>
    /// Apply damage to the player.
    /// </summary>
    /// <param name="amount">The amount of damage to apply to the player.</param>
    public void PlayerTakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    void OnChangeHealth(float health)
    {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        if (hudHealthBar != null)
        {
            Debug.Log("NOT IN HERE!2");
            // apply changes to the health HUD
            hudHealthBar.value = CalculateHealth();
            healthText.text = currentHealth.ToString("F0");
        }
    }

    float CalculateHealth()
    {
        return currentHealth / maxHealth;
    }
}
