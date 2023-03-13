using UnityEngine;

namespace _Scripts.UI.Buttons.Shop_Buttons.AbilitiesButtons
{
    public class AbilityManager : MonoBehaviour
    {
        [SerializeField] private AbilityButton mineField;
        [SerializeField] private AbilityButton barbedWire;
        [SerializeField] private AbilityButton airBombing;
        [SerializeField] private AbilityButton poweredAmmo;
        [SerializeField] private AbilityButton ricochetAmmo;

        public AbilityButton MineField => mineField;
        public AbilityButton BarbedWire => barbedWire;
        public AbilityButton AirBombing => airBombing;
        public AbilityButton PoweredAmmo => poweredAmmo;
        public AbilityButton RicochetAmmo => ricochetAmmo;
    }
}