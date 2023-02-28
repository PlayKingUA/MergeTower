using UnityEngine;

namespace _Scripts.Tower_Logic
{
    [CreateAssetMenu(fileName ="Tower", menuName = "Data/TowerData")]
    public class TowerData : ScriptableObject
    {
        [SerializeField] private int slotPlaces;
        
        public int SlotPlaces => slotPlaces;
    }
}