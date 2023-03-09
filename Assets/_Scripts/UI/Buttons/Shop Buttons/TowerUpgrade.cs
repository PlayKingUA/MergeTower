using UnityEngine;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    public class TowerUpgrade : UpgradeButtonBase
    {
        public override void BuyItem()
        {
            base.BuyItem();
            //ToDo upgrade tower
        }
        
        
        protected override void ChangeButtonState()
        {
            Debug.Log("IsMaxLevel " + IsMaxLevel);
            base.ChangeButtonState();
        }
    }
}