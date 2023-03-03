using System;
using System.Collections.Generic;
using _Scripts.UI.Upgrade;
using _Scripts.Units;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Scripts.Tower_Logic
{
    public class AttackZone : MonoBehaviour
    {
        #region Variables
        [ShowInInspector, ReadOnly] private float _attackRadius;

        public List<Zombie> targetZombies = new();

        private float _yScale;

        [Inject] private UpgradeMenu _upgradeMenu;
        #endregion

        #region Monobehaviour Callbacks
        private void Start()
        {
            _yScale = transform.localScale.y;
            
            _upgradeMenu.RangeUpgrade.OnLevelChanged += UpdateRadius;
            UpdateRadius();
        }

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
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _attackRadius);
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

        private void UpdateRadius()
        {
            var targetDiameter = _upgradeMenu.RangeUpgrade.CurrentValue * 2;
            transform.localScale = new Vector3(targetDiameter, _yScale, targetDiameter);
        }
        
        private void RemoveZombie(Zombie zombie)
        {
            targetZombies.Remove(zombie);
        }
    }
}