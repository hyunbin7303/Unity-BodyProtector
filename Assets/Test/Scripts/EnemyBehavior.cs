using Message;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IMessageReceiver
{
    protected Transform target = null;
    protected EnemyController controller;


    void OnEnable()
    {
        controller = GetComponent<EnemyController>();
        //target = PlayerManager.instance.player.transform;
    }


    public void FindTarget()
    {

    }


    public void OnReceiveMessage(MessageType type, object sender, object msg)
    {
        switch (type)
        {
            case Message.MessageType.DEAD:
                Death();
                break;
            case Message.MessageType.DAMAGED:
                ApplyDamage(/*(Damageable.DamageMessage)msg*/);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Object is dead.
    /// </summary>
    public void Death()
    {
        Debug.Log("[DEBUG - EnemyBehavior.cs]: Enemy had died!");
        Destroy(gameObject);
    }

    public void ApplyDamage()
    {
        // Put code here if doing any effects like...
        // camera shaking, push object after hit, animation, audio, ui update.
    }
}
