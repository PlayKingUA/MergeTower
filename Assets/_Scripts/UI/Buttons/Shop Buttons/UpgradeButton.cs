using TMPro;
using UnityEngine;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    public class UpgradeButton : UpgradeButtonBase
    {
        #region Variables
        [SerializeField] private int levelsPerTowerLevel = 5;
        [SerializeField] private float startValue;
        [SerializeField] private float maxValue;

        [Space(10)] 
        [SerializeField] private TextMeshProUGUI valueBefore;
        [SerializeField] private GameObject moreText;
        [SerializeField] private TextMeshProUGUI valueAfter;
        [SerializeField] private BuyProgressBar buyProgressBar;
        #endregion

        #region Properties
        public float CurrentValue => GetValue(CurrentLevel);
        private float GetValue(int level) => Mathf.Lerp(startValue, maxValue, (float) level / maxLevel);

        public override bool IsMaxLevel =>
            (CurrentLevel / levelsPerTowerLevel) >= UpgradeMenu.TowerLevel.CurrentLevel + 1;

        public int ProgressBarLevel => IsMaxLevel 
            ? levelsPerTowerLevel 
            : CurrentLevel % levelsPerTowerLevel;
        #endregion

        protected override void Start()
        {
            base.Start();
            UpgradeMenu.TowerLevel.OnLevelChanged += ChangeButtonState;
            ChangeButtonState();
        }

        protected override void UpdateInfo()
        {
            base.UpdateInfo();
            
            moreText.SetActive(CanBeBought);
            valueAfter.gameObject.SetActive(CanBeBought);
            
            valueBefore.text = CurrentValue.ToString(".##");
            valueAfter.text = GetValue(CurrentLevel+ 1).ToString(".##");
            
            buyProgressBar.SetActiveToggles(ProgressBarLevel);
        }
    }
}