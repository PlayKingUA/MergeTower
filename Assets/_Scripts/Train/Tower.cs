using System;
using _Scripts.Game_States;
using _Scripts.Interface;
using _Scripts.Slot_Logic;
using _Scripts.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Scripts.Train
{
    public class Tower : MonoBehaviour, IAlive
    {
        #region Variables
        [SerializeField] private float health;

        [Inject] private GameStateManager _gameStateManager;
        [Inject] private SpeedUpLogic _speedUpLogic;
        [Inject] private SlotManager _slotManager;
        [Inject] private WeaponManager _weaponManager;

        [ShowInInspector, ReadOnly] public float MaxHealth { get; private set;}
        public float CurrentHealth { get; private set; }
        public bool IsDead { get; private set;}

        public event Action HpChanged;
        #endregion

        #region Monobehaviour Callbacks
        private void Start()
        {
            _weaponManager.OnNewWeapon += UpdateMaxHealth;
            UpdateMaxHealth();
        }
        #endregion

        private void UpdateMaxHealth()
        {
            MaxHealth = health + _slotManager.WeaponsHealthSum;
            CurrentHealth = MaxHealth;
            HpChanged?.Invoke();
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