using UnityEngine;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    public class TowerUpgrade : UpgradeButtonBase
    {
        protected override void ChangeButtonState()
        {
            Debug.Log("Is max level of tower " + IsMaxLevel);
            base.ChangeButtonState();
        }
    }
}