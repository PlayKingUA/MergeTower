using _Scripts.Tower_Logic;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class ItemsInstaller : MonoInstaller
    {
        [SerializeField] private Tower tower;
        [SerializeField] private AttackZone attackZone;

        public override void InstallBindings()
        {
            
            Container.Bind<Tower>().FromInstance(tower).AsSingle().NonLazy();
            Container.Bind<AttackZone>().FromInstance(attackZone).AsSingle().NonLazy();
        }
    }
}