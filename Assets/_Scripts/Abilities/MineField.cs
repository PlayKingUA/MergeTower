using System;
using System.Collections;
using System.Linq;
using _Scripts.Game_States;
using _Scripts.UI.Buttons.Shop_Buttons.AbilitiesButtons;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Scripts.Abilities
{
    public class MineField : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float radius;

        [Serializable]
        private class MineStats
        {
            [SerializeField] private Mine prefab;
            [SerializeField] private int amount;
            [SerializeField] private int damage;
            [SerializeField] private float damageRadius = -1;
            [SerializeField] private float respawnTime;

            public Mine Mine => prefab;
            public int Amount => amount;
            public int Damage => damage;
            public float DamageRadius => damageRadius;
            public float RespawnTime => respawnTime;
        }

        [SerializeField]
        private MineStats[] mineStats;

        private MineStats _currentStats;
        private bool[] _slots;
        
        private Coroutine _spawnCoroutine;

        [Inject] private AbilityManager _abilityManager;
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private DiContainer _diContainer;
        #endregion

        private void Start()
        {
            _gameStateManager.AttackStarted += () =>
            {
                if (gameObject.activeSelf == false)
                    return;
                
                if (_abilityManager.MineField.CurrentLevel > 0)
                {
                    StartSpawn();
                }
            };
            _gameStateManager.Fail += StopSpawn;
            _gameStateManager.Victory += StopSpawn;
        }

        private void StartSpawn()
        {
            _currentStats = mineStats[_abilityManager.MineField.CurrentLevel];
            _slots = new bool[_currentStats.Amount];
            
            _spawnCoroutine = StartCoroutine(SpawningCoroutine());
        }

        private IEnumerator SpawningCoroutine()
        {
            
            var wait = new WaitForSeconds(_currentStats.RespawnTime);

            while (true)
            {
                yield return wait;
                CreateBomb();
            }
            // ReSharper disable once IteratorNeverReturns
        }

        private void CreateBomb()
        {
            var freeSlots = _slots.Count(t => t == false);
            if (freeSlots == 0)
                return;

            int targetIndex;
            do
            {
                targetIndex = Random.Range(0, _slots.Length);
            } while (_slots[targetIndex]);

            _slots[targetIndex] = true;
            
            
            var bomb = _diContainer.InstantiatePrefabForComponent<Mine>(_currentStats.Mine, transform);
            bomb.Init(_currentStats.Damage, _currentStats.DamageRadius);
        }

        private void StopSpawn()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
