using System;
using QFSW.MOP2;
using UnityEngine;

namespace _Scripts.Abilities
{
    public class AirBombing : AbilityBehaviour
    {
        [Serializable]
        private class BombStats
        {
            [SerializeField] private ObjectPool pool;
            [SerializeField] private int damage;
            [SerializeField] private float damageRadius = -1;
            [SerializeField] private float respawnTime;

            public ObjectPool Projectile => pool;
            public int Damage => damage;
            public float DamageRadius => damageRadius;
            public float RespawnTime => respawnTime;
        }

        [SerializeField]
        private BombStats[] stats;

        protected override void Init()
        {
            TargetAbility = AbilityManager.AirBombing;
        }
    }
}
