using _Scripts.UI.Buttons.Shop_Buttons;
using UnityEngine;

public class UpgradeTowerPanel : MonoBehaviour
{
    [SerializeField] private BuyProgressBar towerProgressBar;
    [SerializeField] private UpgradeButton rangeUpgrade;
    [SerializeField] private UpgradeButton healthUpgrade;
    [SerializeField] private TowerUpgrade towerUpgrade;
    private bool CanUpgrade => rangeUpgrade.IsMaxLevel && healthUpgrade.IsMaxLevel;
    
    private void Start()
    {
        rangeUpgrade.OnLevelChanged += UpdateState;
        healthUpgrade.OnLevelChanged += UpdateState;
        towerUpgrade.OnLevelChanged += UpdateState;
        UpdateState();
    }

    private void UpdateState()
    {
        var currentProgress = rangeUpgrade.ProgressBarLevel + healthUpgrade.ProgressBarLevel;
        towerProgressBar.SetActiveToggles(currentProgress);
        
        towerUpgrade.gameObject.SetActive(CanUpgrade);
        towerProgressBar.gameObject.SetActive(!CanUpgrade);
    }
}
