using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    [RequireComponent(typeof(Toggle))]
    public class AbilityButton : UpgradeButtonBase
    {
        #region Variables
        [Space(10)]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private string abilityName;
        [SerializeField, TextArea] private string description;
        [Space(10)]
        [SerializeField] private TextMeshProUGUI requiredLevelText;
        [SerializeField] private int requiredTowerLevel;
        [SerializeField] private BuyProgressBar buyProgressBar;
        public string AbilityName => abilityName;
        public string Description => description;

        public override bool CanBeBought => base.CanBeBought && !IsMaxLevel && !IsLocked;
        private bool IsLocked => requiredTowerLevel > UpgradeMenu.TowerLevel.CurrentLevel;
        #endregion

        protected override void Start()
        {
            UpgradeMenu.TowerLevel.OnLevelChanged += ChangeButtonState;
            base.Start();
        }

        private void OnValidate()
        {
            nameText.text = abilityName;
        }

        protected override void ChangeButtonState()
        {
            if (IsLocked)
            {
                UpdateInfo();
                buttonStateManager.SetUIState(ButtonBuyState.Locked);
                return;
            }
            base.ChangeButtonState();
        }

        protected override void UpdateInfo()
        {
            base.UpdateInfo();

            requiredLevelText.text = $"LVL {requiredTowerLevel}";
            buyProgressBar.SetActiveToggles(CurrentLevel);
        }
    }
}