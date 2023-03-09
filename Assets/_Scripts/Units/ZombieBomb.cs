using System;
using UnityEngine;

namespace _Scripts.Units
{
    public class ZombieBomb : MonoBehaviour
    {
        [SerializeField] private Zombie targetZombie;
        [SerializeField] private int damage;
        [SerializeField] private float damageRadius;
        [SerializeField] private ParticleSystem fire;

        private void Start()
        {
            targetZombie.DeadEvent += BlowUp;
        }

        private void BlowUp(Zombie obj)
        {
            var damagePoint = transform;
            var _colliders = Physics.OverlapSphere(damagePoint.position, damageRadius);

            for (var i = 0; i < _colliders.Length; i++)
            {
                if (_colliders[i] == null || !_colliders[i].TryGetComponent(out Zombie zombie)) continue;
                zombie.GetDamage(damage);
            }
            
            fire.Play();
        }
        
        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, damageRadius);
        }
    }
}