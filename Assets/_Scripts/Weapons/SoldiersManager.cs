using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Scripts.Weapons
{
    public class SoldiersManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private List<Weapon> soldiers;

        [Inject] private DiContainer _diContainer;
        public int MaxWeaponLevel => soldiers.Count - 1;
        
        public event Action OnNewWeapon;
        #endregion
    

        public Weapon CreateWeapon(int level, Transform parent)
        {
            var weapon = _diContainer.InstantiatePrefabForComponent<Weapon>(soldiers[level], parent);
            weapon.SetLevel(level);
            OnNewWeapon?.Invoke();
            return weapon;
        }
    }
}
