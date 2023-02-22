using UnityEngine;

namespace _Scripts.Train
{
    public class AttackZone : MonoBehaviour
    {
        [SerializeField] protected float attackRadius;
        
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}