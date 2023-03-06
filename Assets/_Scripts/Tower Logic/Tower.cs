using System;
using _Scripts.Cameras;
using _Scripts.Game_States;
using _Scripts.Interface;
using _Scripts.UI.Upgrade;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using CameraType = _Scripts.Cameras.CameraType;

namespace _Scripts.Tower_Logic
{
    public class Tower : MonoBehaviour, IAlive
    {
        #region Variables
        [SerializeField] private TowerLevel[] levelsData;
        [Space(10)]
        [SerializeField] private GameObject hpCanvas;
        
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private UpgradeMenu _upgradeMenu;
        [Inject] private CameraManager _cameraManager;
        
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
            _upgradeMenu.HealthUpgrade.OnLevelChanged += UpdateMaxHealth;
            UpdateMaxHealth();

            _upgradeMenu.TowerLevel.OnLevelChanged += ChangeMesh;
            ChangeMesh();
            
            hpCanvas.SetActive(false);
            _gameStateManager.AttackStarted += () => { hpCanvas.SetActive(true); };
        }
        #endregion

        private void UpdateMaxHealth()
        {
            MaxHealth = _upgradeMenu.HealthUpgrade.CurrentValue;
            CurrentHealth = MaxHealth;
            HpChanged?.Invoke();
        }

        private void ChangeMesh()
        {
            for (var i = 0; i < levelsData.Length; i++)
            {
                EnableTowerLevel(levelsData[i], i == CurrentLevel);
            }
        }

        private void EnableTowerLevel(TowerLevel tower, bool isEnabled)
        {
            tower.gameObject.SetActive(isEnabled);
            
            if (!isEnabled)
                return;
            
            _cameraManager.SetCamera(CameraType.Menu, tower.MenuCamera);
            _cameraManager.SetCamera(CameraType.Abilities, tower.AbilityCamera);
            _cameraManager.SetCamera(CameraType.Attack, tower.GameCamera);
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