using _Scripts.Slot_Logic;
using UnityEngine;
using Zenject;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    public class BuyWeaponButton : ShopButton
    {
        #region Variables
        [SerializeField] private int weaponLevel;
        [Inject] private SlotManager _slotManager;
        #endregion

        public override bool CanBeBought => base.CanBeBought && _slotManager.HasFreePlace();

        public override void BuyItem()
        {
            base.BuyItem();
            _slotManager.CreateNewWeapon(weaponLevel - 1);
        }
    }
}