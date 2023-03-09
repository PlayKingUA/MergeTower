using _Scripts.UI.Buttons.Shop_Buttons;
using UnityEngine;
using Zenject;

namespace _Scripts.UI.Upgrade
{
    public class UpgradeTowerPanel : MonoBehaviour
    {
        [SerializeField] private BuyProgressBar towerProgressBar;

        [Inject] private UpgradeMenu _upgradeMenu;
        
        private UpgradeButton RangeUpgrade => _upgradeMenu.RangeUpgrade;
        private UpgradeButton HealthUpgrade => _upgradeMenu.HealthUpgrade;
        private TowerUpgrade TowerUpgrade => _upgradeMenu.TowerLevel;
        private bool CanUpgrade => RangeUpgrade.IsMaxLevel && HealthUpgrade.IsMaxLevel;
    
        private void Start()
        {
            RangeUpgrade.OnLevelChanged += UpdateState;
            HealthUpgrade.OnLevelChanged += UpdateState;
            TowerUpgrade.OnLevelChanged += UpdateState;
            UpdateState();
        }

        private void UpdateState()
        {
            var currentProgress = RangeUpgrade.ProgressBarLevel + HealthUpgrade.ProgressBarLevel;
            towerProgressBar.SetActiveToggles(currentProgress);
        
            TowerUpgrade.gameObject.SetActive(CanUpgrade);
            towerProgressBar.gameObject.SetActive(!CanUpgrade);
        }
    }
}
