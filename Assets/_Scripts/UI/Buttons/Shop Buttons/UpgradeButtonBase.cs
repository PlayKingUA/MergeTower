using _Scripts.UI.Upgrade;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    public class UpgradeButtonBase : ShopButton
    {
        #region Variables
        [Space(10)] 
        [SerializeField] protected int maxLevel;

        [Inject] protected UpgradeMenu UpgradeMenu;

        public override bool CanBeBought => base.CanBeBought && !IsMaxLevel;
        public virtual bool IsMaxLevel => CurrentLevel >= maxLevel;
        #endregion
        
        protected override void ChangeButtonState()
        {
            if (IsMaxLevel)
            {
                buttonStateManager.SetUIState(ButtonBuyState.MaxLevel);
                UpdateInfo();
                return;
            }
            base.ChangeButtonState();
        }
    }
}