using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Game_States;
using _Scripts.Levels;
using _Scripts.Money_Logic;
using _Scripts.UI.Displays;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Scripts.Units
{
    public class ZombieManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float spawnRadius;
        [SerializeField] private Zombie[] usualZombie;
        [SerializeField] private Zombie[] fastZombie;
        [SerializeField] private Zombie[] bigZombie;
        [SerializeField] private Zombie[] bombers;
        [Space]
        [SerializeField] private ZombieTable zombieTable;
        [SerializeField] private float messageTimeBeforeLastWave = 2f;
        [SerializeField] private float tapMessageDelayAfterLastWave = 0.5f;

        private List<Wave> _zombiesWaves;
        private int _zombiesLeft;

        [Inject] private GameStateManager _gameStateManager;
        [Inject] private LevelManager _levelManager;
        [Inject] private MoneyWallet _moneyWallet;
        [Inject] private DiContainer _diContainer;

        private Coroutine _creatingCoroutine;

        public readonly List<Zombie> AliveZombies = new ();
        public List<Zombie> DeadZombies { get; } = new ();
        public float WholeHpSum { get; private set; }
        public float LostHp { get; private set; }
        public float HpToLastWave { get; private set;}
        public float Progress => LostHp / WholeHpSum;
        
        public event Action OnHpChanged;
        public event Action LastWaveStarted;
        public event Action HugeWaveMessage;
        #endregion
        
        #region Monobehaviour Callbacks
        private void Start()
        {
            _gameStateManager.AttackStarted += StartCreatingZombies;
            _gameStateManager.Fail += ZombieWin;
        }
        #endregion

        public void Init(Level currentLevel)
        {
            _zombiesWaves = currentLevel.ZombiesWaves;

            foreach (var zombieWave in _zombiesWaves)
            {
                if (zombieWave == _zombiesWaves[^1])
                    HpToLastWave = WholeHpSum;

                foreach (var subWave in zombieWave.subWaves)
                {
                    _zombiesLeft += subWave.ZombieCount.UsualZombieCount;
                    _zombiesLeft += subWave.ZombieCount.FastZombieCount;
                    _zombiesLeft += subWave.ZombieCount.BigZombieCount;
                    _zombiesLeft += subWave.ZombieCount.BomberCount;

                    WholeHpSum += subWave.ZombieCount.UsualZombieCount *
                                  usualZombie[0].StartHp(_levelManager.CurrentLevel);
                    WholeHpSum += subWave.ZombieCount.FastZombieCount *
                                  fastZombie[0].StartHp(_levelManager.CurrentLevel);
                    WholeHpSum += subWave.ZombieCount.BigZombieCount * bigZombie[0].StartHp(_levelManager.CurrentLevel);
                    WholeHpSum += subWave.ZombieCount.BomberCount * bombers[0].StartHp(_levelManager.CurrentLevel);
                }
            }
            
            zombieTable.UpdatePanel(currentLevel.ZombieCount);
        }

        private void UpdateLostHp(int deltaHp)
        {
            LostHp += deltaHp;
            OnHpChanged?.Invoke();
        }

        #region Zombie Spawn
        private void StartCreatingZombies()
        {
            _creatingCoroutine = StartCoroutine(CreateZombies());
        }

        private IEnumerator CreateZombies()
        {
            foreach (var zombieWave in _zombiesWaves)
            {
                if (zombieWave == _zombiesWaves[^1])
                    StartCoroutine(LasWaveStartedEvent());
                
                foreach (var subWave in zombieWave.subWaves)
                {
                    var usualZombieLeft = subWave.ZombieCount.UsualZombieCount;
                    var fastZombieLeft = subWave.ZombieCount.FastZombieCount;
                    var bigZombieLeft = subWave.ZombieCount.BigZombieCount;
                    var bombersLeft = subWave.ZombieCount.BomberCount;
                    var zombiesLeft = usualZombieLeft + fastZombieLeft + bigZombieLeft + bombersLeft;
                    
                    while (zombiesLeft-- > 0)
                    {
                        ZombieType zombieType;
                        while (true)
                        {
                            zombieType = (ZombieType) Random.Range(0, (int) ZombieType.CountTypes);
                            if (zombieType == ZombieType.Usual && usualZombieLeft > 0)
                            {
                                usualZombieLeft--;
                                break;
                            }
                            if (zombieType == ZombieType.Fast && fastZombieLeft > 0)
                            {
                                fastZombieLeft--;
                                break;
                            }
                            if (zombieType == ZombieType.Big && bigZombieLeft > 0)
                            {
                                bigZombieLeft--;
                                break;
                            }
                            if (zombieType == ZombieType.Bomber && bombersLeft > 0)
                            {
                                bombersLeft--;
                                break;
                            }
                            
                        }
                        
                        CreateZombie(GetTargetZombie(zombieType));
                        yield return new WaitForSeconds(subWave.TimeBetweenZombie);
                    }
                    yield return new WaitUntil(() => AliveZombies.Count == 0);
                    yield return new WaitForSeconds(subWave.TimeBetweenWaves);
                }
                yield return new WaitUntil(() => AliveZombies.Count == 0);
                yield return new WaitForSeconds(zombieWave.TimeBetweenWaves - messageTimeBeforeLastWave);
                if (zombieWave == _zombiesWaves[^2])
                    HugeWaveMessage?.Invoke();
                yield return new WaitForSeconds(messageTimeBeforeLastWave);
            }
        }

        private IEnumerator LasWaveStartedEvent()
        {
            yield return new WaitForSeconds(tapMessageDelayAfterLastWave);
            LastWaveStarted?.Invoke();
        }

        private void CreateZombie(Zombie targetZombie)
        {
            var zombie = _diContainer.InstantiatePrefabForComponent<Zombie>(targetZombie, GetSpawnPosition(transform.position),
                Quaternion.identity, transform);

            zombie.DeadEvent += RemoveZombie;
            zombie.GetDamageEvent += UpdateLostHp;
            zombie.GetDamageEvent += value =>
            {
                _moneyWallet.Add(value);
            };
            
            AliveZombies.Add(zombie);
        }

        private Zombie GetTargetZombie(ZombieType zombieType)
        {
            Zombie targetZombie;
            switch (zombieType)
            {
                case ZombieType.Usual:
                    targetZombie = usualZombie[Random.Range(0, usualZombie.Length)];
                    break;
                case ZombieType.Fast:
                    targetZombie = fastZombie[Random.Range(0, fastZombie.Length)];
                    break;
                case ZombieType.Big:
                    targetZombie = bigZombie[Random.Range(0, bigZombie.Length)];
                    break;
                case ZombieType.Bomber:
                    targetZombie = bombers[Random.Range(0, bombers.Length)];
                    break;
                case ZombieType.CountTypes:
                default:
                    throw new ArgumentOutOfRangeException(nameof(zombieType), zombieType, null);
            }
            return targetZombie;
        }

        private Vector3 GetSpawnPosition(Vector3 center)
        {
            var angle = Random.Range(0, 360f);
            return new Vector3(
                center.x + spawnRadius * Mathf.Sin(angle * Mathf.Deg2Rad),
                center.y,
                center.z + spawnRadius * Mathf.Cos(angle * Mathf.Deg2Rad)
                );
            
        }
        #endregion

        private void RemoveZombie(Zombie zombie)
        {
            AliveZombies.Remove(zombie);
            DeadZombies.Add(zombie);
            zombieTable.RemoveZombie(zombie.ZombieType);
            _zombiesLeft--;
            
            if (_zombiesLeft <= 0 && AliveZombies.Count == 0)
            {
                _gameStateManager.ChangeState(GameState.Victory);
            }
        }
        
        private void ZombieWin()
        {
            if (_creatingCoroutine != null) 
                StopCoroutine(_creatingCoroutine);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
    }
}
