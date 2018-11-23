using UnityEngine;

/// <summary>
/// A special type of damager. Any contact with the object
/// will apply damage to the target.
/// </summary>
/// <remarks>
/// !IMPORTANT! 
/// Requires the collider set to onTrigger. Also remember to place
/// object in a layer that collides with what you want to damage.
/// (eg. Enemy layer does not collide with Player layer, so add it to child)
/// 
/// Enemy (EmptyObject;Layer=Enemy)
///  Body
///  AttackRoot (EmptyObject;Script=MeleeWeapon)
///  BodyDamager (EmptyObject;Layer=Collider;Script=ContactDamager;BoxCollider)
/// </remarks>
public class ContactDamager : MonoBehaviour
{
    [Tooltip("The damage amount inflicted upon target.")]
    public int damageAmount = 1;

    [Tooltip("Unity Layer that is applicable to be damaged.")]
    public LayerMask damagedLayers;

    private void OnTriggerStay(Collider other)
    {
        if ((damagedLayers.value & 1 << other.gameObject.layer) == 0)
            return;

        Damageable d = other.GetComponentInChildren<Damageable>();

        if (d != null)
        {
            Damageable.DamageMessage message = new Damageable.DamageMessage
            {
                damageSource = transform.position, // The target who will be damaged
                damager = this, // Who is damaging the target
                amount = damageAmount, // Amount of damage inflicted on target
                direction = (other.transform.position - transform.position).normalized,
            };

            d.ApplyDamage(message);
        }
    }
}
