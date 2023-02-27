using TMPro;
using UnityEngine;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    public class UpgradeButton : BuyButton
    {
        #region Variables
        [SerializeField] private float startValue;
        [SerializeField] private float maxUpgrade;

        [Space(10)] 
        [SerializeField] private TextMeshProUGUI valueBefore;
        [SerializeField] private GameObject moreText;
        [SerializeField] private TextMeshProUGUI valueAfter;
        [SerializeField] private BuyProgressBar buyProgressBar;
        #endregion

        #region Properties
        public float CurrentValue => GetValue(CurrentLevel);
        private float GetValue(int level) 
            => startValue + (maxUpgrade - startValue) * ((float) level / maxLevel);

        protected override bool CanBeBought => base.CanBeBought && CurrentValue < maxUpgrade;
        #endregion
        
        protected override void ChangeButtonState(float moneyCount)
        {
            if (CurrentValue >= maxUpgrade)
            {
                _buttonState = ButtonBuyState.MaxLevel;
                SetUIState(_buttonState);
                return;
            }
            base.ChangeButtonState(moneyCount);
        }

        protected override void ClickEvent()
        {
            base.ClickEvent();
            ChangeButtonState(MoneyWallet.MoneyCount);
        }

        protected override void UpdateText()
        {
            base.UpdateText();
            
            moreText.SetActive(CanBeBought);
            valueAfter.gameObject.SetActive(CanBeBought);
            
            valueBefore.text = CurrentValue.ToString(".##");
            valueAfter.text = GetValue(CurrentLevel+ 1).ToString(".##");
            
            buyProgressBar.SetActiveToggles(CurrentLevel);
        }
    }
}