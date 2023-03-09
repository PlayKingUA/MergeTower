using _Scripts.Money_Logic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    [RequireComponent(typeof(Button))]
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private ShopButton targetButton;
        private Button _button;
        
        [Inject] protected MoneyWallet MoneyWallet;

        public ShopButton TargetButton
        {
            set => targetButton = value;
        }

        protected void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }

        private void Click()
        {
            if (!targetButton.CanBeBought)
                return;
            
            targetButton.BuyItem();
        }

        public void SetInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;
        }
    }
}
