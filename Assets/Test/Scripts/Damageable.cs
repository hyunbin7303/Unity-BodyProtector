using Message;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Damageable class is for objects that can take damage.
/// </summary>
public class Damageable : MonoBehaviour
{
    // The maximum HP of the character, enemy, etc.
    public int maxHitPoints;

    [Tooltip("The angle from which the object is hitable.")]
    [Range(0.0f, 360.0f)]
    public float hitAngle = 360.0f;

    public int currentHitPoints { get; private set; }

    [Tooltip("When this gameObject is damaged, these other gameObjects are notified.")]
    //[EnforceType(typeof(Message.IMessageReceiver))]
    public List<MonoBehaviour> onDamageMessageReceivers;

    System.Action schedule;

    void Start()
    {

    }

    void LateUpdate()
    {
        if (schedule != null)
        {
            schedule();
            schedule = null;
        }
    }


    public void ApplyDamage(DamageMessage data)
    {
        // Ignore any damage. It is already dead
        if (currentHitPoints <= 0) {
            return;
        }

        // Decrease the hit points
        currentHitPoints -= data.amount;

        // If hitpoints <= 0, then dead
        // otherwise object receives damage

        // Get the message type
        var messageType = currentHitPoints <= 0 ? MessageType.DEAD : MessageType.DAMAGED;

        // Notify the object with a message
        for (int i = 0; i < onDamageMessageReceivers.Count; i++)
        {
            var receiver = onDamageMessageReceivers[i] as IMessageReceiver;
            // Apply the message type and data (damage) to this object...
            receiver.OnReceiveMessage(messageType, this, data);
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        
    }
#endif

    /// <summary>
    /// Message containing information about damage.
    /// </summary>
    public struct DamageMessage
    {
        public MonoBehaviour damager;
        public int amount;
        public Vector3 direction;
        public Vector3 damageSource;

        public bool stopCamera;
    }
}
