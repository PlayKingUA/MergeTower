using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Units
{
    public class AttackingObject : MonoBehaviour
    {
        #region Variables
        [SerializeField] private int damage;
        [SerializeField] protected float attackSpeedPerSecond;
        [ShowInInspector, ReadOnly] protected float AttackTimer;
        #endregion

        #region Properties

        protected virtual float CoolDown => 1f / attackSpeedPerSecond;

        protected virtual int Damage
        {
            set => damage = value;
            get => damage;
        }
        #endregion
        
        #region Monobehaviour Callbacks
        protected virtual void Start(){}

        protected virtual void Update(){}
        protected virtual void FixedUpdate()
        {
            AttackTimer += Time.fixedDeltaTime;
        }
        #endregion
    }
}