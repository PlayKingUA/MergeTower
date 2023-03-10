using System.Collections;
using _Scripts.Units;
using QFSW.MOP2;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Projectiles
{
    public sealed class Projectile : MonoBehaviour
    {
        #region Variables
        [SerializeField] private bool isSplash;
        [SerializeField, ShowIf(nameof(isSplash))]
        private float damageRadius;
        [Space(10)]
        [SerializeField]
        private float speed;
        [SerializeField] private bool hasImpact = true;
        [SerializeField, ShowIf(nameof(hasImpact))]
        private ObjectPool impactPool;

        [ShowInInspector, ReadOnly] 
        private int _damage;

        private MasterObjectPooler _masterObjectPooler;
        private ObjectPool _projectilePool;

        private const float LifeTime = 3.0f;

        private Coroutine _flyRoutine;
        #endregion

        #region Monobehavior Callbacks
        private void Awake()
        {
            _masterObjectPooler = MasterObjectPooler.Instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (hasImpact)
            {
                var transform1 = transform;
                _masterObjectPooler.GetObject(impactPool.PoolName, transform1.position, transform1.rotation);
            }

            if (isSplash)
            {
                SplashHit();
            }
            else if (other.TryGetComponent(out Zombie zombie))
            {
                zombie.GetDamage(_damage);
                ReturnToPool();
            }
        }

        private void OnDrawGizmos()
        {
            if (!isSplash)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, damageRadius);
        }
        #endregion

        public void Init(int damage, ObjectPool objectPool, Zombie targetZombie = null)
        {
            _projectilePool = objectPool;
            _damage = damage;
            
            _flyRoutine = StartCoroutine(FlyToTarget(targetZombie));
        }

        private IEnumerator FlyToTarget(Zombie targetZombie)
        {
            var flyTime = 0f;
            var direction = transform.forward;
            
            while (true)
            {
                if (targetZombie != null && !targetZombie.IsDead)
                {
                    direction = (targetZombie.ShootPoint.position - transform.position).normalized;
                }

                transform.position += direction * (speed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(direction);

                flyTime += Time.deltaTime;
                yield return null;

                if (flyTime < LifeTime) continue;
                ReturnToPool();
                yield break;
            }
        }

        private void ReturnToPool()
        {
            if(_flyRoutine != null) 
                StopCoroutine(_flyRoutine);
            
            _masterObjectPooler.Release(gameObject, _projectilePool.PoolName);
        }

        private void SplashHit(Transform damagePoint = null)
        {
            if (damagePoint == null)
            {
                damagePoint = transform;
            }

            var colliders = Physics.OverlapSphere(damagePoint.position, damageRadius);
            for (var i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] == null || !colliders[i].TryGetComponent(out Zombie zombie)) continue;
                zombie.GetDamage(_damage);
                if (!isSplash)
                    break;
            }
            
            ReturnToPool();
        }
    }
}