using System;
using System.Collections;
using _Scripts.Game_States;
using _Scripts.Helpers;
using _Scripts.Interface;
using _Scripts.Levels;
using _Scripts.Tower_Logic;
using QFSW.MOP2;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Scripts.Units
{
    [RequireComponent(typeof(ZombieAnimationManager), 
        typeof(RagdollController), typeof(UnitMovement))]
    public sealed class Zombie : AttackingObject, IAlive
    {
        #region Variables
        [SerializeField] private int health;
        [SerializeField] private float hpPerLevel;
        [SerializeField] private float dmgPerLevel;
        [Space(10)]
        [SerializeField] private ZombieType zombieType;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject dustObject;
        [Space]
        //[SerializeField] private Color damageColor;
        [SerializeField] private ObjectPool damageText;
        [Space]
        [ShowInInspector, ReadOnly] private UnitState _currentState;

        private ZombieAnimationManager _zombieAnimationManager;
        private RagdollController _ragdollController;
        private MasterObjectPooler _masterObjectPooler;
        
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private LevelManager _levelManager;
        [Inject] private Tower _tower;

        private const float DamageAnimationDuration = 0.15f;

        private UnitMovement _unitMovement;
        
        public bool IsDead { get; private set; }

        public event Action<int> GetDamageEvent;
        public event Action<Zombie> DeadEvent;
        #endregion

        #region Properties
        private int Health
        {
            set => health = value;
            get => health;
        }
        
        public Transform ShootPoint => shootPoint;
        public ZombieType ZombieType => zombieType;
        
        public int StartHp(int currentLevel) => (int) (Health + (currentLevel - 1) * hpPerLevel);

        #endregion

        #region Monobehaviour Callbacks
        private void Awake()
        {
            _masterObjectPooler = MasterObjectPooler.Instance;
            _zombieAnimationManager = GetComponent<ZombieAnimationManager>();
            _ragdollController = GetComponent<RagdollController>();
            _unitMovement = GetComponent<UnitMovement>();
        }

        protected override void Start()
        {
            base.Start();
            ChangeState(UnitState.Run);
            
            Health = (int) (Health + (_levelManager.CurrentLevel - 1) * hpPerLevel);
            Damage = (int) (Damage + (_levelManager.CurrentLevel - 1) * dmgPerLevel);
        }

        protected override void Update()
        {
            base.Update();
            UpdateState();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TowerLevel tower))
                ChangeState(UnitState.Attack);
        }
        #endregion
        
        #region States Logic
        public void ChangeState(UnitState newState)
        {
            if (IsDead)
                return;

            _currentState = newState;
            if (_currentState != UnitState.Attack)
            {
                _zombieAnimationManager.SetAnimation(_currentState);
            }
            else
            {
                _unitMovement.StopMotion();
            }
        }

        private void UpdateState()
        {
            if (IsDead)
                return;

            if (_currentState == UnitState.Attack)
            {
                AttackState();
            }
        }

        private void AttackState()
        {
            if (_gameStateManager.CurrentState == GameState.Fail)
            {
                ChangeState(UnitState.Victory);
            }
            
            if (AttackTimer < CoolDown) 
                return;

            _zombieAnimationManager.SetAnimation(_currentState);
            AttackTimer = 0f;
        }
        #endregion

        public void Attack()
        {
            _tower.GetDamage(Damage);
        }
        
        #region Get Damage\Die
        public void GetDamage(int damagePoint)
        {
            if (IsDead)
                return;
            
            var damage = Mathf.Min(Health, damagePoint);
            Health -= damage;
            GetDamageEvent?.Invoke(damage);
            CreateDamageText(damage);
            //_coinsAnimation.CollectCoins(shootPoint, damage);

            if (Health <= 0)
                Die();
            
            /*for (var i = 0; i < _materials.Length; i++)
            {
                _damageTweens[i].Rewind();
                _damageTweens[i].Kill();
                _damageTweens[i] = _materials[i].DOColor(damageColor, "_Color", DamageAnimationDuration)
                    .SetLoops(2, LoopType.Yoyo);
            }*/
        }

        public void Die()
        {
            if (IsDead)
                return;

            IsDead = true;

            DeadEvent?.Invoke(this);
            _unitMovement.StopMotion();
            DisableDust();
            
            _zombieAnimationManager.DisableAnimator();
            _ragdollController.EnableRagdoll(true);

            StartCoroutine(DestroyObject());
        }

        private void CreateDamageText(int damage)
        {
            var text =
                _masterObjectPooler.GetObjectComponent<DamageText>(damageText.PoolName, shootPoint.position, transform.rotation);
            text.SetText(damage.ToString());
        }
        #endregion

        private IEnumerator DestroyObject()
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }

        private void DisableDust()
        {
            dustObject.SetActive(false);
        }
    }
}
