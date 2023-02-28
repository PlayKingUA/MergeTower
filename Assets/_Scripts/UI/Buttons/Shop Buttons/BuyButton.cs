using System;
using _Scripts.Money_Logic;
using _Scripts.UI.Displays;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    [RequireComponent(typeof(Button))]
    public class BuyButton : MonoBehaviour
    {
        #region Variables
        [ShowInInspector] private ButtonBuyState _buttonState;
        [SerializeField] private GameObject[] states;
        [SerializeField] private string saveKey = "WeaponPrice";
        [Space(10)]
        [SerializeField] protected int startPrice;
        [SerializeField] protected int baseAmount;
        [SerializeField] protected float multiplier;
        [SerializeField] protected float powMultiplier;
        [SerializeField] private TextMeshProUGUI priseText;

        private Button _button;

        [ShowInInspector, ReadOnly]
        private int _currentLevel;

        public int CurrentLevel
        {
            get => _currentLevel;
            private set
            {
                _currentLevel = value; 
                OnLevelChanged?.Invoke();
            }
        }

        [Inject] protected MoneyWallet MoneyWallet;

        public event Action OnBought;
        public event Action OnLevelChanged;
        #endregion

        #region Properties
        private ButtonBuyState ButtonState => _buttonState;

        private int CurrentPrise => GetPrise(CurrentLevel);
        public bool IsEnoughMoney => MoneyWallet.MoneyCount >= CurrentPrise;

        protected virtual bool CanBeBought => IsEnoughMoney;
        #endregion
    
        #region Monobehaviour Callbacks
        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Click);
            Load();
        }

        protected virtual void Start()
        {
            UpdateText();

            MoneyWallet.MoneyCountChanged += CheckMoney;
            CheckMoney(MoneyWallet.MoneyCount);
        }
        #endregion
        
        #region Display
        protected virtual void ChangeButtonState()
        {
            SetUIState((MoneyWallet.MoneyCount >= CurrentPrise)
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

        protected virtual void UpdateText()
        {
            priseText.text = MoneyDisplay.MoneyText(CurrentPrise);
        }
        #endregion

        #region Click
        private void Click()
        {
            if (!CanBeBought)
                return;
            
            switch (ButtonState)
            {
                case ButtonBuyState.BuyWithMoney:
                    MoneyWallet.Get(CurrentPrise);
                    ClickEvent();
                    break;
                case ButtonBuyState.BuyWithADs:
                    break;
                case ButtonBuyState.MaxLevel:
                    return;
                default:
                    break;
            }
        }

        protected virtual void ClickEvent()
        {
            CurrentLevel++;
            Save();
            
            OnBought?.Invoke();
            ChangeButtonState();
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

        private void CheckMoney(float moneyCount)
        {
            ChangeButtonState();
        }

        public void SetInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;
        }
        private int GetPrise(int level)
        {
            if (level == 0)
            {
                return startPrice;
            }

            return GetPrise(level - 1) + 
                   (int) (multiplier * baseAmount * Mathf.Pow(level + 1,powMultiplier));
        }
    }
}
