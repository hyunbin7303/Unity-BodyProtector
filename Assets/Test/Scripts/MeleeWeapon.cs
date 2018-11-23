using System;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damage = 1;

    [Serializable]
    public class AttackPoint
    {
        public float radius;
        public Vector3 offset;
        public Transform attackBubble;
    }

    // The point(s) of where the attack takes place and can inflict damage to the opponent
    public AttackPoint[] attackPoints = new AttackPoint[0];

    
    // The owner of the melee weapon
    protected GameObject _owner;


    public void SetOwner(GameObject owner)
    {
        _owner = owner;
    }



#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < attackPoints.Length; ++i)
        {
            AttackPoint pts = attackPoints[i];

            if (pts.attackBubble != null)
            {
                Vector3 worldPos = pts.attackBubble.TransformVector(pts.offset);
                Gizmos.color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
                Gizmos.DrawSphere(pts.attackBubble.position + worldPos, pts.radius);
            }

            //if (pts.previousPositions.Count > 1)
            //{
            //    UnityEditor.Handles.DrawAAPolyLine(10, pts.previousPositions.ToArray());
            //}
        }
    }
#endif
}
