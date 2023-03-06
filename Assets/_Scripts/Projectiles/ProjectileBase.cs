using _Scripts.Units;
using QFSW.MOP2;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Projectiles
{
    public class ProjectileBase : MonoBehaviour
    {
        #region Variables
        [SerializeField] protected bool isSplash;
        [SerializeField, ShowIf(nameof(isSplash))] protected float damageRadius;
        [ShowInInspector, ReadOnly] 
        protected int Damage;
        
        protected Vector3 LaunchPosition;
        protected Zombie TargetZombie;

        private Collider[] _colliders;
        
        protected MasterObjectPooler MasterObjectPooler;
        protected ObjectPool ProjectilePool;
        #endregion
        
        #region Monobehavior Callbacks
        protected virtual void Awake()
        {
            MasterObjectPooler = MasterObjectPooler.Instance;
        }
        protected virtual void Start(){}
        protected virtual  void OnTriggerEnter(Collider other){}
        #endregion
        
        public virtual void Init(Zombie targetZombie, int damage, ObjectPool objectPool)
        {
            ProjectilePool = objectPool;
            LaunchPosition = transform.position;
            TargetZombie = targetZombie;
            Damage = damage;
        }

        protected virtual void ReturnToPool() => MasterObjectPooler.Release(gameObject, ProjectilePool.PoolName);

        protected virtual void SplashHit(Transform damagePoint = null)
        {
            if (damagePoint == null) damagePoint = transform;
            
            _colliders = Physics.OverlapSphere(damagePoint.position, damageRadius);

            for (var i = 0; i < _colliders.Length; i++)
            {
                if (_colliders[i] == null || !_colliders[i].TryGetComponent(out Zombie zombie)) continue;
                zombie.GetDamage(Damage);
                if (!isSplash)
                    break;
            }
            
            ReturnToPool();
        }
        
        protected virtual void OnDrawGizmos()
        {
            if (!isSplash)
                return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, damageRadius);
        }
    }
}