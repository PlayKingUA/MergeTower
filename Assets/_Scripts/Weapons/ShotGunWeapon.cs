using _Scripts.Projectiles;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class ShotGunWeapon : PistolWeapon
    {
        [Space(10)]
        [SerializeField] private int bulletCount;
        [SerializeField] private int otherProjectilesDamage;
        [SerializeField] private float angleBetweenProjectiles = 5f;
        
        protected override void Fire()
        {
            base.Fire();
            
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
            
            var yDegrees = shootPoint.rotation.eulerAngles.y + degrees;
            var bullet = MasterObjectPooler.GetObjectComponent<Projectile>(projectilePool.PoolName,
                shootPoint.position, Quaternion.Euler(0f, yDegrees, 0f));

            bullet.Init(TargetZombie, otherProjectilesDamage, projectilePool);
            return bullet;
        }
    }
}