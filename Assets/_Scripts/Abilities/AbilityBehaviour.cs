using _Scripts.UI.Buttons.Shop_Buttons.AbilitiesButtons;
using UnityEngine;
using Zenject;

namespace _Scripts.Abilities
{
    public abstract class AbilityBehaviour : MonoBehaviour
    {
        [SerializeField]
        protected GameObject[] targetMesh;

        [Inject] protected AbilityManager AbilityManager;
        
        protected AbilityButton TargetAbility;

        protected bool IsBought => TargetAbility.CurrentLevel > 0;
        
        protected virtual void Start()
        {
            Init();
            TargetAbility.OnLevelChanged += UpdateMesh;
            UpdateMesh();
        }

        protected abstract void Init();
        
        private void UpdateMesh()
        {
            foreach (var mesh in targetMesh)
            {
                mesh.SetActive(false);
            }

            if (IsBought == false)
                return;
            
            var meshLevel = Mathf.Min(TargetAbility.CurrentLevel - 1, targetMesh.Length - 1);
            targetMesh[meshLevel].SetActive(true);
        }
    }
}