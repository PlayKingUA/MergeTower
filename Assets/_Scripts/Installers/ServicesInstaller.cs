using _Scripts.Cameras;
using _Scripts.Game_States;
using _Scripts.Input_Logic;
using _Scripts.Levels;
using _Scripts.Money_Logic;
using _Scripts.Slot_Logic;
using _Scripts.Tutorial;
using _Scripts.UI.Buttons.Shop_Buttons.AbilitiesButtons;
using _Scripts.Units;
using _Scripts.Weapons;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class ServicesInstaller : MonoInstaller
    {
        [SerializeField] private MoneyWallet moneyWallet;
        [SerializeField] private SlotManager slotManager;
        [SerializeField] private SoldiersManager soldiersManager;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private DragManager dragManager;
        [SerializeField] private ZombieManager zombieManager;
        [SerializeField] private GameStateManager gameStateManager;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private SpeedUpLogic speedUpLogic;
        
        [SerializeField] private LevelGeneration levelGeneration;
        [SerializeField] private TutorialManager tutorialManager;
        [SerializeField] private VibrationManager vibrationManager;
        
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private AbilityManager abilityManager;
        
        public override void InstallBindings()
        {
            Container.Bind<MoneyWallet>().FromInstance(moneyWallet).AsSingle().NonLazy();
            Container.Bind<SlotManager>().FromInstance(slotManager).AsSingle().NonLazy();
            Container.Bind<SoldiersManager>().FromInstance(soldiersManager).AsSingle().NonLazy();
            Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle().NonLazy();
            Container.Bind<DragManager>().FromInstance(dragManager).AsSingle().NonLazy();
            Container.Bind<ZombieManager>().FromInstance(zombieManager).AsSingle().NonLazy();
            Container.Bind<GameStateManager>().FromInstance(gameStateManager).AsSingle().NonLazy();
            Container.Bind<LevelManager>().FromInstance(levelManager).AsSingle().NonLazy();
            Container.Bind<SpeedUpLogic>().FromInstance(speedUpLogic).AsSingle().NonLazy();
            Container.Bind<LevelGeneration>().FromInstance(levelGeneration).AsSingle().NonLazy();
            Container.Bind<TutorialManager>().FromInstance(tutorialManager).AsSingle().NonLazy();
            Container.Bind<VibrationManager>().FromInstance(vibrationManager).AsSingle().NonLazy();
            Container.Bind<CameraManager>().FromInstance(cameraManager).AsSingle().NonLazy();
            Container.Bind<AbilityManager>().FromInstance(abilityManager).AsSingle();
        }
    }
}