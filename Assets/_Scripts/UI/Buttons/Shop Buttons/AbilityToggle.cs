using System;
using _Scripts.UI.Upgrade;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    [RequireComponent(typeof(AbilityButton))]
    public class AbilityToggle : MonoBehaviour
    {
        private Toggle _toggle;
        private AbilityButton _abilityButton;
        [Inject] private AbilitiesPanel _abilitiesPanel;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _abilityButton = GetComponent<AbilityButton>();
        }

        private void Start()
        {
            _toggle.onValueChanged.AddListener(SelectAbility);
            _abilityButton.OnLevelChanged += UpdateAbilityPanel;

            if (_toggle.isOn)
            {
                UpdateAbilityPanel();
            }
        }

        private void SelectAbility(bool isOn)
        {
            if (isOn)
                UpdateAbilityPanel();
            
            // set green mesh
        }

        private void UpdateAbilityPanel()
        {
            _abilitiesPanel.UpdateAbility(_abilityButton);
        }
    }
}