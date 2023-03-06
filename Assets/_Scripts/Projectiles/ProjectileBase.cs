using _Scripts.Units;
using QFSW.MOP2;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Projectiles
{
    public class ProjectileBase : MonoBehaviour
    {
        #region Variables
        [ShowInInspector, ReadOnly] 
        protected int Damage;
        
        protected Vector3 LaunchPosition;
        protected Zombie TargetZombie;

        protected MasterObjectPooler MasterObjectPooler;
        private ObjectPool _projectilePool;
        #endregion
        
        #region Monobehavior Callbacks
        protected virtual void Awake()
        {
            MasterObjectPooler = MasterObjectPooler.Instance;
        }
        protected virtual void Start(){}
        protected virtual  void OnTriggerEnter(Collider other){}
        
        protected virtual void OnDrawGizmos(){}
        #endregion
        
        public virtual void Init(Zombie targetZombie, int damage, ObjectPool objectPool)
        {
            _projectilePool = objectPool;
            LaunchPosition = transform.position;
            TargetZombie = targetZombie;
            Damage = damage;
        }

        protected virtual void ReturnToPool() => MasterObjectPooler.Release(gameObject, _projectilePool.PoolName);
    }
}