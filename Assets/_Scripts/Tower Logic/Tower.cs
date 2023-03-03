using System;
using _Scripts.Game_States;
using _Scripts.Interface;
using _Scripts.UI.Upgrade;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Scripts.Tower_Logic
{
    public class Tower : MonoBehaviour, IAlive
    {
        #region Variables
        [SerializeField] private TowerLevel[] levelsData;
        
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private UpgradeMenu _upgradeMenu;
        
        [ShowInInspector, ReadOnly] public float MaxHealth { get; private set;}
        public float CurrentHealth { get; private set; }
        public bool IsDead { get; private set;}

        public TowerLevel CurrentTowerLevel => levelsData[CurrentLevel];
        private int CurrentLevel => _upgradeMenu.TowerLevel.CurrentLevel;
        public event Action HpChanged;
        #endregion

        #region Monobehaviour Callbacks
        private void Start()
        {
            _gameStateManager.AttackStarted += UpdateMaxHealth;
            UpdateMaxHealth();

            _upgradeMenu.TowerLevel.OnLevelChanged += ChangeMesh;
            ChangeMesh();
        }
        #endregion

        private void UpdateMaxHealth()
        {
            MaxHealth = _upgradeMenu.TowerHealth;
            CurrentHealth = MaxHealth;
            HpChanged?.Invoke();
        }

        private void ChangeMesh()
        {
            for (var i = 0; i < levelsData.Length; i++)
            {
                levelsData[i].gameObject.SetActive(i == CurrentLevel);
            }
        }
        
        #region Get Damage\Die
        public void GetDamage(int damageAmount)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - damageAmount);
            HpChanged?.Invoke();

            if (CurrentHealth <= 0 && !IsDead)
                Die();
        }
        
        public void Die()
        {
            if (IsDead)
                return;

            IsDead = true;
            _gameStateManager.ChangeState(GameState.Fail);
        }
        #endregion
    }
}