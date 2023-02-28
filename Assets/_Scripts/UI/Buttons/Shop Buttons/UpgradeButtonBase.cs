using _Scripts.UI.Upgrade;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    public class UpgradeButtonBase : BuyButton
    {
        [Space(10)] 
        [SerializeField] protected int maxLevel;

        [Inject] protected UpgradeMenu UpgradeMenu;
        
        protected override bool CanBeBought => base.CanBeBought && !IsMaxLevel;
        public virtual bool IsMaxLevel => CurrentLevel >= maxLevel;

        [Button("Update State")]
        protected override void ChangeButtonState()
        {
            if (IsMaxLevel)
            {
                SetUIState(ButtonBuyState.MaxLevel);
                return;
            }
            base.ChangeButtonState();
        }
    }
}