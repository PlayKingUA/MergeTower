using _Scripts.UI.Tutorial;
using _Scripts.UI.Upgrade;
using _Scripts.UI.Windows;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private WindowsManager windowsManager;
        [SerializeField] private TutorialWindow tutorialWindow;
        [SerializeField] private UpgradeMenu upgradeMenu;
        [SerializeField] private AbilitiesPanel abilitiesPanel;
        
        //[SerializeField] private CoinsAnimation coinsAnimation;
        
        public override void InstallBindings()
        {
            Container.Bind<WindowsManager>().FromInstance(windowsManager).AsSingle();
            Container.Bind<TutorialWindow>().FromInstance(tutorialWindow).AsSingle();
            Container.Bind<UpgradeMenu>().FromInstance(upgradeMenu).AsSingle();
            Container.Bind<AbilitiesPanel>().FromInstance(abilitiesPanel).AsSingle();
            
            //Container.Bind<CoinsAnimation>().FromInstance(coinsAnimation).AsSingle();
        }   
    }
}