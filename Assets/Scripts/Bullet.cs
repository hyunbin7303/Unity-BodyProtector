using UnityEngine;
using UnityEngine.Networking;

public class Bullet : MonoBehaviour
{
    /// <summary>The bullet belongs to object owner who spawned it. We use it to identify who shot the bullet.</summary>
    public NetworkInstanceId ownerId;

    public float bulletSpeed = 10.0f;
    public float damageRate = 50.0f;

    public float expireRate;
    private float currentTimer;

    public PlayerStats playerStats;
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        currentTimer += Time.deltaTime * 1;

        // After a certain amount of time, destroy the bullet object
        if (currentTimer >= expireRate)
            Destroy(gameObject);
    }


    void OnCollisionEnter(Collision collision)
    {
        var enemyCollidedHit = collision.gameObject;
        if (enemyCollidedHit.CompareTag("Enemy"))
        {
            Debug.Log("ENEMY COLLISION DETECT!");
            DealDamageToEnemy(enemyCollidedHit);
            Destroy(gameObject);
        }
    }


    public void DealDamageToEnemy(GameObject otherObject)
    {
        GameObject player = NetworkServer.FindLocalObject(ownerId);
        damageRate = player.GetComponent<PlayerStats>().Damage;
        DevLog.Log("Bullet", "Player with id <" + ownerId.ToString() + "> dealt damage to enemy with " + damageRate);
        otherObject.GetComponent<Health>().EnemyTakeDamage(damageRate, ownerId);
    }


}
