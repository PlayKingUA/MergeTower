using _Scripts.Game_States;
using _Scripts.Levels;
using _Scripts.UI.Windows;
using _Scripts.Units;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI.Displays
{
    public class ProgressDisplay : MonoBehaviour
    {
        #region Variables
        [SerializeField] private CanvasGroup progressPanel;
        [SerializeField] private TextMeshProUGUI currentLevelText;
        [SerializeField] private TextMeshProUGUI nextLevelText;
        [SerializeField] private Slider progressSlider;
        [Space(10)]
        [SerializeField] private RectTransform bossPointer;
        [SerializeField] private Transform leftPosition;
        [SerializeField] private Transform rightPosition;

        [Inject] private ZombieManager _zombieManager;
        [Inject] private GameStateManager _gameStateManager;
        [Inject] private LevelManager _levelManager;
        #endregion
    
        #region Monobehaviour Callbacks
        private void Awake()
        {
            _zombieManager.OnHpChanged += DisplayProgress;
            
            _gameStateManager.PrepareToBattle += () => { EnableProgressPanel(false);};
            _gameStateManager.AttackStarted += () =>
            {
                EnableProgressPanel(true);
                UpdateBossPosition();
            };

            _levelManager.OnLevelLoaded += UpdateLevelText;
        }

        private void Start()
        {
            progressSlider.value = 0f;
        }
        #endregion

        private void DisplayProgress()
        {
            progressSlider.DOValue(_zombieManager.Progress, 0.1f).SetSpeedBased();
        }

        private void UpdateLevelText(Level level)
        {
            currentLevelText.text = level.index.ToString();
            nextLevelText.text = (level.index + 1).ToString();
        }
        
        private void EnableProgressPanel(bool isEnabled)
        {
            WindowsManager.CanvasGroupSwap(progressPanel, isEnabled);
        }

        private void UpdateBossPosition()
        {
            var targetBossPosition = bossPointer.localPosition;
            targetBossPosition.x = bossPointer.sizeDelta.x / 2
                                   + (rightPosition.position.x - leftPosition.position.x) 
                                   * (_zombieManager.HpToLastWave / _zombieManager.WholeHpSum);
            bossPointer.localPosition = targetBossPosition;
        }
    }
}
