using _Scripts.UI.Buttons.Shop_Buttons;
using UnityEngine;

namespace _Scripts.UI.Upgrade
{
    public class UpgradeMenu : MonoBehaviour
    {
        #region Variables
        [Space(10)]
        [SerializeField] private UpgradeButton rangeUpgrade;
        [SerializeField] private UpgradeButton towerHealth;
        [SerializeField] private UpgradeButton towerLevel;
        #endregion

        #region Properties
        public float DamageCoefficient => rangeUpgrade.Coefficient;
        public float TowerHealth => towerHealth.Coefficient;
        public float TowerLevel => towerLevel.Coefficient;
        #endregion
    }
}