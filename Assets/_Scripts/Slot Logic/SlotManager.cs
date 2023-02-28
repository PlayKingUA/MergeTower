using System;
using System.Collections.Generic;
using _Scripts.Tower_Logic;
using _Scripts.UI.Upgrade;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Scripts.Slot_Logic
{
    public class SlotManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Slot[] slots;
        [Space(10)]
        [SerializeField, ReadOnly] private List<Slot> emptySlots;
        [SerializeField, ReadOnly] private List<Slot> busySlots;

        private bool _isTutorialArrows;
        
        [Inject] private UpgradeMenu _upgradeMenu;
        [Inject] private Tower _tower;
        #endregion

        #region Properties
        public bool HasFreePlace() => emptySlots.Count > 0;
        #endregion

        #region Monobehaviour Callbacks
        private void Start()
        {
            _upgradeMenu.TowerLevel.OnLevelChanged += () =>
            {
                UpdateSlotsInfo();
            };
            UpdateSlotsInfo(true);
        }
        #endregion

        private void UpdateSlotsInfo(bool isInit = false)
        {
            emptySlots.Clear();
            busySlots.Clear();
            
            for (var i = 0; i < slots.Length; i++)
            {
                var slot = slots[i];
                if (isInit)
                    slot.Init();

                var isEnabled = i < _tower.CurrentLevelData.SlotPlaces;
                slot.gameObject.SetActive(isEnabled);
                if (!isEnabled)
                    continue;
                
                switch (slot.SlotState)
                {
                    case SlotState.Empty:
                        emptySlots.Add(slot);
                        break;
                    case SlotState.Busy:
                        busySlots.Add(slot);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            if (_isTutorialArrows)
            {
                ShowTutorialArrows();
            }
        }

        public void CreateNewWeapon(int targetLevel = 0)
        {
            if (!HasFreePlace())
            {
                return;
            }
            
            foreach (var slot in slots)
            {
                if (slot.SlotState != SlotState.Empty) continue;
                slot.SpawnWeapon(targetLevel, true);
                emptySlots.Remove(slot);
                return;
            }
            
            if (_isTutorialArrows)
            {
                ShowTutorialArrows();
            }
        }
        
        public void RefreshSlots(Slot weaponSLot)
        {
            switch (weaponSLot.SlotState)
            {
                case SlotState.Empty:
                    emptySlots.Add(weaponSLot);
                    busySlots.Remove(weaponSLot);
                    break;
                case SlotState.Busy:
                    emptySlots.Remove(weaponSLot);
                    busySlots.Remove(weaponSLot);
                    busySlots.Add(weaponSLot);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (_isTutorialArrows)
            {
                ShowTutorialArrows();
            }
        }
        
        public void ShowTutorialArrows(bool isShown = true)
        {
            _isTutorialArrows = isShown;

            foreach (var slot in slots)
            {
                slot.EnablePointer(_isTutorialArrows);
            }
        }
    }
}