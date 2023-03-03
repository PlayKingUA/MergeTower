using UnityEngine;

namespace _Scripts.Tower_Logic
{
    public class TowerLevel : MonoBehaviour
    {
        [SerializeField] private Transform slotsParent;

        public int SlotPlaces => slotsParent.childCount;

        public Vector3 GetSlotPosition(int index) => slotsParent.GetChild(index).position;
    }
}