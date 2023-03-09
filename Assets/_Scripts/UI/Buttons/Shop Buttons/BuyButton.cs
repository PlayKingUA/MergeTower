using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    [RequireComponent(typeof(Button))]
    public class BuyButton : ShopButton
    {
        #region Variables
        [ShowInInspector] private ButtonBuyState _buttonState;
        [SerializeField] private GameObject[] states;
        
        private Button _button;
        
        private ButtonBuyState ButtonState => _buttonState;
        #endregion

        #region Monobehaviour Callbacks
        protected override void Awake()
        {
            base.Awake();
            
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
        }

        protected override void Start()
        {
            base.Start();
            
            MoneyWallet.MoneyCountChanged += CheckMoney;
            CheckMoney(MoneyWallet.MoneyCount);
        }
        #endregion
        
        #region Display
        protected override void ChangeButtonState()
        {
            SetUIState(IsEnoughMoney
                ? ButtonBuyState.BuyWithMoney
                : ButtonBuyState.BuyWithADs);
        }

        protected void SetUIState(ButtonBuyState targetState)
        {
            _buttonState = targetState;
            foreach (var state in states)
            {
                state.SetActive(false);
            }
            
            //when we'll have ads
            //states[(int)targetState].SetActive(true);
            _button.interactable = targetState != ButtonBuyState.BuyWithADs;
            states[(int)ButtonBuyState.BuyWithMoney].SetActive(targetState != ButtonBuyState.MaxLevel);
            states[(int)ButtonBuyState.MaxLevel].SetActive(targetState == ButtonBuyState.MaxLevel);
            
            UpdateText();
        }
        #endregion

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
        
        private void CheckMoney(float moneyCount)
        {
            ChangeButtonState();
        }

        public override void SetInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;
        }
    }
}
