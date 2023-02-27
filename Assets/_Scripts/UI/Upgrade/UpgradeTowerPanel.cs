using System.Collections;
using System.Collections.Generic;
using _Scripts.UI.Buttons.Shop_Buttons;
using UnityEngine;

public class UpgradeTowerPanel : MonoBehaviour
{
    [SerializeField] private BuyProgressBar towerProgressBar;
    [SerializeField] private UpgradeButton rangeUpgrade;
    [SerializeField] private UpgradeButton healthUpgrade;
    [SerializeField] private UpgradeButton TowerUpgrade;
    
    private void Start()
    {
        rangeUpgrade.OnLevelChanged += UpdateState;
        healthUpgrade.OnLevelChanged += UpdateState;
        UpdateState();
    }

    private void UpdateState()
    {
        var currentProgress = rangeUpgrade.CurrentLevel + healthUpgrade.CurrentLevel;
        towerProgressBar.SetActiveToggles(currentProgress);
    }
}
