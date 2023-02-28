using UnityEngine;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    public class TowerUpgrade : UpgradeButtonBase
    {
        protected override void ClickEvent()
        {
            base.ClickEvent();
            //ToDo upgrade tower
        }
        
        
        protected override void ChangeButtonState()
        {
            Debug.Log("IsMaxLevel " + IsMaxLevel);
            base.ChangeButtonState();
        }
    }
}