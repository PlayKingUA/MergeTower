using System;
using _Scripts.Units;
using UnityEngine;

namespace _Scripts.Projectiles
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float damageRadius;
        [SerializeField] private ParticleSystem fire;
        
        public event Action OnBlowUp;
        
        public void Init(int targetDamage, float targetDamageRadius = -1)
        {
            damage = targetDamage;

            if (targetDamageRadius >= 0)
            {
                damageRadius = targetDamageRadius;
            }
        }
        
        protected void BlowUp(Zombie obj)
        {
            var damagePoint = transform;
            var colliders = Physics.OverlapSphere(damagePoint.position, damageRadius);

            for (var i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] == null || !colliders[i].TryGetComponent(out Zombie zombie)) continue;
                zombie.GetDamage(damage);
            }
            
            fire.Play();
            
            OnBlowUp?.Invoke();
        }
        
        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, damageRadius);
        }
    }
}