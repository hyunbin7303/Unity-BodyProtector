using UnityEngine;
using UnityEngine.Networking;

public class Bullet : MonoBehaviour
{
    //private NetworkInstanceId netId;

    public float bulletSpeed = 10.0f;
    public float damageRate = 50.0f;

    public float expireRate;
    private float currentTimer;


    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        currentTimer += Time.deltaTime * 1;

        if (currentTimer >= expireRate)
            Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        var enemyCollidedHit = collision.gameObject;
        //var health = enemyCollidedHit.GetComponent<Health>();
        //if(health != null)
        //{
        //    Debug.Log("Enemy Take damage 50!");
        //    health.TakeDamage(50f/*, netId*/);
        //}

        if (enemyCollidedHit.CompareTag("Enemy"))
        {
            Debug.Log("ENEMY COLLISION DETECT!");
            DealDamageToEnemy(enemyCollidedHit);
            Destroy(gameObject);
        }
    }


    public void DealDamageToEnemy(GameObject otherObject)
    {
        otherObject.GetComponent<Health>().EnemyTakeDamage(50.0f);
    }
}
