using _Scripts.Projectiles;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class ShotGunWeapon : PistolWeapon
    {
        [Space(10)]
        [SerializeField] private int bulletCount;
        [SerializeField] private int otherProjectilesDamage;
        [SerializeField] private float angleBetweenProjectiles = 15f;

        private Quaternion _baseRotation;
        
        protected override void Fire()
        {
            base.Fire();
            
            var direction = (TargetZombie.ShootPoint.position - shootPoint.position).normalized;
            _baseRotation = Quaternion.LookRotation(direction);
            
            // first bullets created in base
            for (var i = 0; i < bulletCount - 1; i++)
            {
                var sign = i % 2 == 0 ? 1 : - 1;
                var degrees = sign * angleBetweenProjectiles * (1 + i / 2);
                CreateProjectile(degrees);
            }
        }

        private Projectile CreateProjectile(float degrees)
        {
            if (hasShells)
            {
                MasterObjectPooler.GetObject(shellsPool.PoolName, shootPoint.position, shootPoint.rotation);
            }
            
            var bullet = MasterObjectPooler.GetObjectComponent<Projectile>(projectilePool.PoolName,
                shootPoint.position, _baseRotation);

            var bulletTransform = bullet.transform;
            bulletTransform.RotateAround(bulletTransform.position, bulletTransform.up, degrees);
            
            bullet.Init(otherProjectilesDamage, projectilePool);
            return bullet;
        }
    }
}