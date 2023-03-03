using _Scripts.UI.Buttons.Shop_Buttons;
using _Scripts.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Upgrade
{
    public class UpgradeMenu : MonoBehaviour
    {
        #region Variables
        [Space(10)]
        [SerializeField] private UpgradeButton rangeUpgrade;
        [SerializeField] private UpgradeButton healthUpgrade;
        [SerializeField] private TowerUpgrade towerLevel;
        
        [Space(10)]
        [SerializeField] private Button openAbilitiesButton;
        [SerializeField] private Button closeAbilitiesButton;
        
        [Space(10)]
        [SerializeField] private CanvasGroup upgradePanel;
        [SerializeField] private CanvasGroup abilitiesPanel;
        #endregion

        #region Properties
        public UpgradeButton RangeUpgrade => rangeUpgrade;
        public UpgradeButton HealthUpgrade => healthUpgrade;
        public TowerUpgrade TowerLevel => towerLevel;
        #endregion
        
        private void Awake()
        {
            openAbilitiesButton.onClick.AddListener(() =>
            {
                ShopWindowSwipe(true);
            });
            closeAbilitiesButton.onClick.AddListener(() =>
            {
                ShopWindowSwipe(false);
            });
        }
        
        private void ShopWindowSwipe(bool isAbilitiesPanel)
        {
            WindowsManager.CanvasGroupSwap(abilitiesPanel, isAbilitiesPanel);
            WindowsManager.CanvasGroupSwap(upgradePanel, !isAbilitiesPanel);
        }
    }
}