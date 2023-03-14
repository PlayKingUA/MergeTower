using System;
using System.Collections;
using System.Linq;
using _Scripts.Game_States;
using _Scripts.UI.Buttons.Shop_Buttons.AbilitiesButtons;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Scripts.Abilities
{
    public class MineField : AbilityBehaviour
    {
        #region Variables
        [SerializeField] private float spawnRadius;

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
        private Vector3 _center;
        
        private Coroutine _spawnCoroutine;

        [Inject] private GameStateManager _gameStateManager;
        [Inject] private DiContainer _diContainer;
        #endregion

        protected override void Start()
        {
            base.Start();
            _center = transform.position;
            
            _gameStateManager.AttackStarted += () =>
            {
                if (isActiveAndEnabled == false)
                    return;
                
                if (IsBought)
                {
                    StartSpawn();
                }
            };
            _gameStateManager.Fail += StopSpawn;
            _gameStateManager.Victory += StopSpawn;
        }

        protected override void Init()
        {
            TargetAbility = AbilityManager.MineField;
        }

        private void StartSpawn()
        {
            _currentStats = mineStats[TargetAbility.CurrentLevel - 1];
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
            
            var angle = (360 / _currentStats.Amount) * targetIndex;
            var spawnPosition = new Vector3(
                _center.x + spawnRadius * Mathf.Sin(angle * Mathf.Deg2Rad),
                _center.y,
                _center.z + spawnRadius * Mathf.Cos(angle * Mathf.Deg2Rad)
            );

            var bomb = _diContainer.InstantiatePrefabForComponent<Mine>(_currentStats.Mine, spawnPosition,
                Quaternion.identity, transform);
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
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}
