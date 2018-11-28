using UnityEngine;
using UnityEngine.Networking;

public class Bullet : MonoBehaviour {

    public NetworkInstanceId netId;

    void OnCollisionEnter(Collision collision)
    {
        //var hit = collision.gameObject;
        //var health = hit.GetComponent<Health>();
        //if (health != null)
        //{
        //    health.TakeDamage(10);
        //}
        var enemyCollidedHit = collision.gameObject;
        var health = enemyCollidedHit.GetComponent<Health>();
        if(health != null)
        {
            Debug.Log("Enemy Take damage 50!");
            health.TakeDamage(50, netId);
        }
        if (enemyCollidedHit.CompareTag("Enemy"))
        {
            Debug.Log("ENEMY COLLISION DETECT!");
            Destroy(gameObject);
        }
    }



}
