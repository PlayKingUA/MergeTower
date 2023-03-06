using _Scripts.Projectiles;
using QFSW.MOP2;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class PistolWeapon : Weapon
    {
        #region Variables
        [Space(10)]
        [SerializeField]
        protected Transform shootPoint;
        [SerializeField] protected ObjectPool projectilePool;
        [SerializeField] protected bool hasShells;
        [SerializeField, ShowIf(nameof(hasShells))]
        protected ObjectPool shellsPool;
        [SerializeField] protected bool hasMuzzleflare = true;
        [SerializeField, ShowIf(nameof(hasMuzzleflare))]
        protected ObjectPool muzzleflarePool;

        protected MasterObjectPooler MasterObjectPooler;
        #endregion

        #region Monobehaviour Callbacks
        protected override void Start()
        {
            MasterObjectPooler =MasterObjectPooler.Instance;
            base.Start();
        }
        #endregion

        #region States
        protected override void AttackState()
        {
            base.AttackState();
            
            if (!CanAttack) 
                return;

            Fire();
            AttackTimer = 0f;
        }
        #endregion
        
        protected virtual void Fire()
        {
            if (hasMuzzleflare)
            {
                MasterObjectPooler.GetObject(muzzleflarePool.PoolName, shootPoint.position, shootPoint.rotation);
            }
            if (hasShells)
            {
                MasterObjectPooler.GetObject(shellsPool.PoolName, shootPoint.position, shootPoint.rotation);
            }

            var bullet = MasterObjectPooler.GetObjectComponent<Projectile>(projectilePool.PoolName, shootPoint.position,
                shootPoint.rotation);

            bullet.Init(TargetZombie, Damage, projectilePool);
            WeaponAnimator.SetAnimation(SoldierState.Attack);
        }
    }
}