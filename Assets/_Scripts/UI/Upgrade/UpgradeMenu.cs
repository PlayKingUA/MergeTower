using _Scripts.UI.Buttons.Shop_Buttons;
using UnityEngine;

namespace _Scripts.UI.Upgrade
{
    public class UpgradeMenu : MonoBehaviour
    {
        #region Variables
        [Space(10)]
        [SerializeField] private UpgradeButton rangeUpgrade;
        [SerializeField] private UpgradeButton healthUpgrade;
        [SerializeField] private UpgradeButton towerUpgrade;
        #endregion

        #region Properties
        public float DamageCoefficient => rangeUpgrade.Coefficient;
        public float AltSpeedCoefficient => healthUpgrade.Coefficient;
        public float IncomeCoefficient => towerUpgrade.Coefficient;

        #endregion
    }
}