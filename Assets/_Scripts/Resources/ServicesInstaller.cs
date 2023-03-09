using _Scripts.Cameras;
using _Scripts.Game_States;
using _Scripts.Input_Logic;
using _Scripts.Levels;
using _Scripts.Money_Logic;
using _Scripts.Slot_Logic;
using _Scripts.Tower_Logic;
using _Scripts.Tutorial;
using _Scripts.UI;
using _Scripts.UI.Tutorial;
using _Scripts.UI.Upgrade;
using _Scripts.UI.Windows;
using _Scripts.Units;
using _Scripts.Weapons;
using UnityEngine;
using Zenject;

namespace _Scripts.Resources
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private SlotManager slotManager;
        [SerializeField] private MoneyWallet moneyWallet;
        [SerializeField] private SoldiersManager soldiersManager;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private DragManager dragManager;
        [SerializeField] private WindowsManager windowsManager;
        [SerializeField] private ZombieManager zombieManager;
        [SerializeField] private GameStateManager gameStateManager;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private Tower tower;
        [SerializeField] private AttackZone attackZone;
        [SerializeField] private SpeedUpLogic speedUpLogic;
        [SerializeField] private UpgradeMenu upgradeMenu;
        [SerializeField] private LevelGeneration levelGeneration;
        [SerializeField] private TutorialManager tutorialManager;
        [SerializeField] private TutorialWindow tutorialWindow;
        [SerializeField] private VibrationManager vibrationManager;
        [SerializeField] private CoinsAnimation coinsAnimation;
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private AbilitiesPanel abilitiesPanel;
        
        public override void InstallBindings()
        {
            Container.Bind<SlotManager>().FromInstance(slotManager).AsSingle().NonLazy();
            Container.Bind<MoneyWallet>().FromInstance(moneyWallet).AsSingle().NonLazy();
            Container.Bind<SoldiersManager>().FromInstance(soldiersManager).AsSingle().NonLazy();
            Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle().NonLazy();
            Container.Bind<DragManager>().FromInstance(dragManager).AsSingle().NonLazy();
            Container.Bind<WindowsManager>().FromInstance(windowsManager).AsSingle().NonLazy();
            Container.Bind<ZombieManager>().FromInstance(zombieManager).AsSingle().NonLazy();
            Container.Bind<GameStateManager>().FromInstance(gameStateManager).AsSingle().NonLazy();
            Container.Bind<LevelManager>().FromInstance(levelManager).AsSingle().NonLazy();
            Container.Bind<Tower>().FromInstance(tower).AsSingle().NonLazy();
            Container.Bind<AttackZone>().FromInstance(attackZone).AsSingle().NonLazy();
            Container.Bind<SpeedUpLogic>().FromInstance(speedUpLogic).AsSingle().NonLazy();
            Container.Bind<UpgradeMenu>().FromInstance(upgradeMenu).AsSingle().NonLazy();
            Container.Bind<LevelGeneration>().FromInstance(levelGeneration).AsSingle().NonLazy();
            Container.Bind<TutorialManager>().FromInstance(tutorialManager).AsSingle().NonLazy();
            Container.Bind<TutorialWindow>().FromInstance(tutorialWindow).AsSingle().NonLazy();
            Container.Bind<VibrationManager>().FromInstance(vibrationManager).AsSingle().NonLazy();
            Container.Bind<CameraManager>().FromInstance(cameraManager).AsSingle().NonLazy();
            Container.Bind<AbilitiesPanel>().FromInstance(abilitiesPanel).AsSingle();
            
            Container.Bind<CoinsAnimation>().FromInstance(coinsAnimation).AsSingle().NonLazy();
            
        }
    }
}