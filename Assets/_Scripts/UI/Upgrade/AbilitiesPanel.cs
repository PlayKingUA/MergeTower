using _Scripts.UI.Buttons;
using _Scripts.UI.Buttons.Shop_Buttons;
using _Scripts.UI.Displays;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Upgrade
{
    public class AbilitiesPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI abilityNameText;
        [SerializeField] private TextMeshProUGUI abilityDescriptionText;
        [SerializeField] private Button buyButton;
        [SerializeField] private TextMeshProUGUI priceText;
        [Space]
        [SerializeField] private TextMeshProUGUI buyButtonText;
        [SerializeField] private string buyText = "Buy";
        [SerializeField] private string upgradeText = "Upgrade";
        [ShowInInspector] protected ButtonBuyState _buttonState;
        [SerializeField] protected GameObject[] states;
        
        [ShowInInspector, ReadOnly]
        private AbilityButton _currentButton;

        private void Start()
        {
            buyButton.onClick.AddListener(BuyAbility);
        }

        private void BuyAbility()
        {
            if (_currentButton.IsLocked)
                return;
            
            switch (_currentButton.ButtonState)
            {
                case ButtonBuyState.BuyWithMoney:
                    _currentButton.BuyItem();
                    UpdateAbility(_currentButton);
                    break;
                case ButtonBuyState.BuyWithADs:
                    break;
                case ButtonBuyState.MaxLevel:
                    return;
                default:
                    break;
            }
        }

        public void UpdateAbility(AbilityButton abilityButton)
        {
            _currentButton = abilityButton;
            
            abilityNameText.text = _currentButton.AbilityName;
            abilityDescriptionText.text = _currentButton.Description;
            priceText.text = $"{MoneyDisplay.MoneyText(_currentButton.CurrentPrise)}";

            buyButtonText.text = _currentButton.CurrentLevel == 0 ? buyText : upgradeText;
            SetButtonState(abilityButton.ButtonState);
        }

        private void SetButtonState(ButtonBuyState targetState)
        {
            foreach (var state in states)
            {
                state.SetActive(false);
            }

            if (targetState == ButtonBuyState.BuyWithADs)
            {
                states[(int)ButtonBuyState.BuyWithMoney].SetActive(true);
            }
            else
            {
                states[(int)targetState].SetActive(true);
            }
        }
    }
}
