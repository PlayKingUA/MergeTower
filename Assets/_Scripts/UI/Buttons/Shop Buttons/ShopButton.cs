using System;
using _Scripts.Money_Logic;
using _Scripts.UI.Displays;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    public class ShopButton : MonoBehaviour
    {
        #region Variables
        [SerializeField] protected ButtonStateManager buttonStateManager;
        [Space(10)]
        [SerializeField] protected string saveKey = "WeaponPrice";
        [SerializeField] protected PriceGenerator priceGenerator;
        [SerializeField] protected TextMeshProUGUI priseText;
        
        [ShowInInspector, ReadOnly]
        private int _currentLevel;

        [Inject] protected MoneyWallet MoneyWallet;

        public event Action OnLevelChanged;
        public event Action OnBought;
        #endregion

        #region Properties
        public int CurrentPrise => priceGenerator.GetPrise(CurrentLevel);
        public bool IsEnoughMoney => MoneyWallet.MoneyCount >= CurrentPrise;
        public virtual bool CanBeBought => IsEnoughMoney;
        
        public int CurrentLevel
        {
            get => _currentLevel;
            private set => _currentLevel = value;
        }

        public ButtonBuyState CurrentButtonState => buttonStateManager.ButtonState; 
        #endregion

        #region Monobehaviour Callbacks
        protected virtual void Awake()
        {
            Load();
        }

        protected virtual void Start()
        {
            MoneyWallet.MoneyCountChanged += ChangeButtonState;
            ChangeButtonState();
            
            OnLevelChanged?.Invoke();
        }
        #endregion
        
        public virtual void BuyItem()
        {
            MoneyWallet.Get(CurrentPrise);
            CurrentLevel++;

            ChangeButtonState();
            OnBought?.Invoke();
            OnLevelChanged?.Invoke();
            
            Save();
        }
        
        #region Display
        protected virtual void ChangeButtonState()
        {
            buttonStateManager.SetUIState(IsEnoughMoney ? ButtonBuyState.BuyWithMoney : ButtonBuyState.BuyWithADs);
            UpdateInfo();
        }

        protected virtual void UpdateInfo()
        {
            priseText.text = MoneyDisplay.MoneyText(CurrentPrise);
        }
        #endregion
        
        #region Save/Load

        private void Save()
        {
            PlayerPrefs.SetInt(saveKey, CurrentLevel);
        }

        private void Load()
        {
            CurrentLevel = PlayerPrefs.GetInt(saveKey);
        }
        #endregion
    }
}