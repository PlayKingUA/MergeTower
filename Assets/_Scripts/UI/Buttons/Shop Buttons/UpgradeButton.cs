using TMPro;
using UnityEngine;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    public class UpgradeButton : BuyButton
    {
        #region Variables
        [SerializeField] private float startValue;
        [SerializeField] private float upgradeStep;
        [SerializeField] private float maxUpgrade;

        [Space(10)] 
        [SerializeField] private TextMeshProUGUI valueBefore;
        [SerializeField] private GameObject moreText;
        [SerializeField] private TextMeshProUGUI valueAfter;
        #endregion

        #region Properties
        public float Coefficient => GetValue(CurrentLevel);

        protected override bool CanBeBought => base.CanBeBought && Coefficient < maxUpgrade;
        #endregion
        
        protected override void ChangeButtonState(float moneyCount)
        {
            if (Coefficient >= maxUpgrade)
            {
                _buttonState = ButtonBuyState.MaxLevel;
                SetUIState(_buttonState);
                return;
            }
            base.ChangeButtonState(moneyCount);
        }
        
        private float GetValue(int level)
        {
            return startValue + upgradeStep * level;
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
            
            valueBefore.text = $"{Coefficient}";
            valueAfter.text = $"{GetValue(CurrentLevel+ 1)}";
        }
    }
}