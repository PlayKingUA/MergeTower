﻿using System.Collections.Generic;
using _Scripts.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Tower_Logic
{
    public class AttackZone : MonoBehaviour
    {
        #region Variables
        [ShowInInspector, ReadOnly] private float _attackRadius;

        public List<Zombie> targetZombies = new();
        #endregion

        #region Monobehaviour Callbacks
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Someone in attack zone");
            if (other.TryGetComponent<Zombie>(out var zombie) == false)
                return;
            
            targetZombies.Add(zombie);
            zombie.DeadEvent += RemoveZombie;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
        #endregion
        
        public Zombie GetNearestZombie(Transform fromTransform)
        {
            Zombie targetZombie = null;
            var minDistance = 1e9f;
            
            foreach (var zombie in targetZombies)
            {
                var currentDistance = Vector3.Distance(fromTransform.position, zombie.ShootPoint.position);
                if (!(currentDistance < minDistance)) continue;
                minDistance = currentDistance;
                targetZombie = zombie;
            }

            return targetZombie;
        }
        
        private void RemoveZombie(Zombie zombie)
        {
            targetZombies.Remove(zombie);
        }
    }
}