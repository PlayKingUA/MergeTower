using _Scripts.UI.Buttons.Shop_Buttons;
using _Scripts.UI.Upgrade;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI.Buttons
{
    [RequireComponent(typeof(Toggle))]
    public class AbilityButton : ShopButton
    {
        #region Variables
        [Space(10)]
        [SerializeField] private TextMeshProUGUI nameText;
        [Space(10)]
        [SerializeField] private string abilityName;
        [SerializeField, TextArea] private string description;
        [Space(10)]
        [SerializeField] private int requiredTowerLevel;
        [SerializeField] private BuyProgressBar buyProgressBar;
        
        private Toggle _toggle;

        [Inject] private AbilitiesPanel _abilitiesPanel;

        public string AbilityName => abilityName;
        public string Description => description;
        public int RequiredTowerLevel => requiredTowerLevel;
        #endregion

        #region Monobehaviour Callbacks
        protected override void Awake()
        {
            base.Awake();
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(SelectAbility);

            if (_toggle.isOn)
            {
                _abilitiesPanel.UpdateAbility(this);
            }
        }

        private void OnValidate()
        {
            nameText.text = abilityName;
        }
        #endregion

        private void SelectAbility(bool isOn)
        {
            if (isOn)
                _abilitiesPanel.UpdateAbility(this);
            
            // green mesh
        }
        
        protected override void UpdateInfo()
        {
            base.UpdateInfo();
            
            buyProgressBar.SetActiveToggles(CurrentLevel);
        }
    }
}