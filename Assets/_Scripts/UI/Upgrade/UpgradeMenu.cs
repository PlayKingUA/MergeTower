using _Scripts.UI.Buttons.Shop_Buttons;
using UnityEngine;

namespace _Scripts.UI.Upgrade
{
    public class UpgradeMenu : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] private UpgradeButton rangeUpgrade;
        [SerializeField] private UpgradeButton healthUpgrade;
        [SerializeField] private TowerUpgrade towerLevel;
        
        public UpgradeButton RangeUpgrade => rangeUpgrade;
        public UpgradeButton HealthUpgrade => healthUpgrade;
        public TowerUpgrade TowerLevel => towerLevel;
        
    }
}