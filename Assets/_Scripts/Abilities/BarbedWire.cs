using System;
using _Scripts.Game_States;
using _Scripts.UI.Buttons.Shop_Buttons.AbilitiesButtons;
using UnityEngine;
using Zenject;

namespace _Scripts.Abilities
{
    public class BarbedWire : MonoBehaviour
    {
        #region Variables
        [Serializable]
        private class WireStats
        {
            [SerializeField] private float damagePerSecond;
            [SerializeField] private float slowdownPower;

            public float DamagePerSecond => damagePerSecond;
            public float SlowdownPower => slowdownPower;
        }

        [SerializeField] private WireStats[] wireStats;
        
        private float _currentDamagePerSecond;
        private float _currentSlowdownPower;
        
        public float DamagePerSecond => _currentDamagePerSecond;
        public float SlowdownPower => _currentSlowdownPower;

        [Inject] private AbilityManager _abilityManager;
        [Inject] private GameStateManager _gameStateManager;
        #endregion

        private void Start()
        {
            _gameStateManager.AttackStarted += UpdateStats;
        }

        private void UpdateStats()
        {
            var currentStats = wireStats[_abilityManager.BarbedWire.CurrentLevel];
            _currentDamagePerSecond = currentStats.DamagePerSecond;
            _currentSlowdownPower = currentStats.SlowdownPower;
        }
    }
}


