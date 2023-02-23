using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.UI.Displays
{
    public class HpDisplay : MonoBehaviour
    {
        #region Variables
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private Slider slider;
        [Inject] private Tower_Logic.Tower _tower;
        #endregion
        
        #region Monobehaviour Callbacks
        private void Awake()
        {
            _tower.HpChanged += Display;
        }
        #endregion
        
        private void Display()
        {
            hpText.text = _tower.CurrentHealth + "/" + _tower.MaxHealth;
            slider.value = _tower.CurrentHealth / _tower.MaxHealth;
        }
    }
}