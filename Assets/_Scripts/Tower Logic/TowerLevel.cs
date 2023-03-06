using Cinemachine;
using UnityEngine;

namespace _Scripts.Tower_Logic
{
    public class TowerLevel : MonoBehaviour
    {
        [SerializeField] private Transform slotsParent;
        [SerializeField] private CinemachineVirtualCamera menuCamera;
        [SerializeField] private CinemachineVirtualCamera abilityCamera;
        [SerializeField] private CinemachineVirtualCamera gameCamera;

        public int SlotPlaces => slotsParent.childCount;

        public CinemachineVirtualCamera MenuCamera => menuCamera;
        public CinemachineVirtualCamera AbilityCamera => abilityCamera;
        public CinemachineVirtualCamera GameCamera => gameCamera;

        public Vector3 GetSlotPosition(int index) => slotsParent.GetChild(index).position;
    }
}