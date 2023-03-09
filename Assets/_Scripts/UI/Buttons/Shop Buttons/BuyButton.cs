using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    [RequireComponent(typeof(Button))]
    public class BuyButton : ShopButton
    {
        #region Variables
        private Button _button;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }

        private void Click()
        {
            if (!CanBeBought)
                return;
            
            switch (ButtonState)
            {
                case ButtonBuyState.BuyWithMoney:
                    MoneyWallet.Get(CurrentPrise);
                    BuyItem();
                    break;
                case ButtonBuyState.BuyWithADs:
                    break;
                case ButtonBuyState.MaxLevel:
                    return;
                default:
                    break;
            }
        }

        public override void SetInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;
        }
    }
}
