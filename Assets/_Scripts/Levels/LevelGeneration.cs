using System.Collections;
using System.Collections.Generic;
using _Scripts.Units;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Scripts.Levels
{
    public class LevelGeneration : MonoBehaviour
    {
        #region Variables
        [ShowInInspector] private LocationType _locationType;
        [SerializeField] private GameObject[] lights;
        [SerializeField] private LevelLocation[] locations;
        
        [Inject] private DiContainer _diContainer;
        #endregion
        
        public void SetLocation(LocationType locationType)
        {
            _locationType = locationType;
            var location = locations[(int) _locationType];
            
            var environment = _diContainer.InstantiatePrefabForComponent<LevelLocation>(location, transform);
            
            for (var i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(false);
                lights[i].SetActive(i == (int) locationType);
            }
        }
    }
}
