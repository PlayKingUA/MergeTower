using System;
using _Scripts.UI.Buttons;
using _Scripts.UI.Buttons.Shop_Buttons;
using _Scripts.UI.Buttons.Shop_Buttons.AbilitiesButtons;
using _Scripts.UI.Displays;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace _Scripts.UI.Upgrade
{
    public class AbilitiesPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI abilityNameText;
        [SerializeField] private TextMeshProUGUI abilityDescriptionText;
        [SerializeField] private BuyButton buyButton;
        [SerializeField] private TextMeshProUGUI priceText;
        [Space]
        [SerializeField] private TextMeshProUGUI buyButtonText;
        [SerializeField] private string buyText = "Buy";
        [SerializeField] private string upgradeText = "Upgrade";
        [SerializeField] private ButtonStateManager buyButtonStates;

        public void UpdateAbility(AbilityButton abilityButton)
        {
            buyButton.TargetButton = abilityButton;
            buyButtonStates.SetUIState(abilityButton.CurrentButtonState);
            buyButtonText.text = abilityButton.CurrentLevel == 0 ? buyText : upgradeText;
            
            abilityNameText.text = abilityButton.AbilityName;
            abilityDescriptionText.text = abilityButton.Description;
            priceText.text = $"{MoneyDisplay.MoneyText(abilityButton.CurrentPrise)}";
        }
    }
}
