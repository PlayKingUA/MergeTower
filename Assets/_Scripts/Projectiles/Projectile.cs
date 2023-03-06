using System.Collections;
using _Scripts.Units;
using DG.Tweening;
using QFSW.MOP2;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Projectiles
{
    public class Projectile : ProjectileBase
    {
        #region Variables
        [SerializeField] protected bool isSplash;
        [SerializeField, ShowIf(nameof(isSplash))]
        protected float damageRadius;
        [Space(10)]
        [SerializeField] protected float speed;
        [SerializeField] protected bool hasImpact = true;
        [SerializeField, ShowIf(nameof(hasImpact))]
        protected ObjectPool impactPool;

        private Tweener _motionTween;

        private const float LifeTime = 3.0f;

        private Coroutine _flyRoutine;
        #endregion

        #region Monobehavior Callbacks
        protected override void Awake()
        {
            MasterObjectPooler = MasterObjectPooler.Instance;
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (hasImpact)
            {
                MasterObjectPooler.GetObject(impactPool.PoolName, transform.position, transform.rotation);
            }

            if (isSplash)
            {
                SplashHit();
            }
            else if (other.TryGetComponent(out Zombie zombie))
            {
                zombie.GetDamage(Damage);
                ReturnToPool();
            }
        }
        
        protected override void OnDrawGizmos()
        {
            if (!isSplash)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, damageRadius);
        }
        #endregion

        public override void Init(Zombie targetZombie, int damage, ObjectPool objectPool)
        {
            base.Init(targetZombie, damage, objectPool);
            _flyRoutine = StartCoroutine(FlyToTarget(targetZombie));
        }

        private IEnumerator FlyToTarget(Zombie targetZombie)
        {
            float flyTime = 0;
            var direction = (targetZombie.ShootPoint.position - LaunchPosition).normalized;
            
            while (true)
            {
                if (targetZombie.IsDead)
                {
                    transform.position += direction * (speed * Time.deltaTime);
                }
                else
                {
                    _motionTween.Kill();
                    _motionTween = transform.DOMove(targetZombie.ShootPoint.position, speed).SetSpeedBased();
                }

                transform.rotation = Quaternion.LookRotation(direction);

                flyTime += Time.deltaTime;
                yield return null;

                if (flyTime < LifeTime) continue;
                ReturnToPool();
                yield break;
            }
        }
        
        protected override void ReturnToPool()
        {
            if(_flyRoutine != null) 
                StopCoroutine(_flyRoutine);
            base.ReturnToPool();
        }
        
        protected virtual void SplashHit(Transform damagePoint = null)
        {
            if (damagePoint == null) damagePoint = transform;
            
            var _colliders = Physics.OverlapSphere(damagePoint.position, damageRadius);

            for (var i = 0; i < _colliders.Length; i++)
            {
                if (_colliders[i] == null || !_colliders[i].TryGetComponent(out Zombie zombie)) continue;
                zombie.GetDamage(Damage);
                if (!isSplash)
                    break;
            }
            
            ReturnToPool();
        }
    }
}