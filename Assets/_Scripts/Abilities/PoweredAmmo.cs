using System;
using UnityEngine;

namespace _Scripts.Abilities
{
    public class PoweredAmmo : AbilityBehaviour
    {
        #region Variables
        [Serializable]
        private class Stats
        {
        }

        [SerializeField] private Stats[] _stats;
        #endregion


        protected override void Init()
        {
            TargetAbility = AbilityManager.PoweredAmmo;
        }
    }
}
