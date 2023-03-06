using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Buttons
{
    [RequireComponent(typeof(Toggle))]
    public class AbilityButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameText;
        
        [SerializeField] private string abilityName;
        [SerializeField, TextArea] private string description;
        [Space(10)]
        [SerializeField] private int requiredTowerLevel;
        
        private Toggle _toggle;
        
        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            //_toggle.onValueChanged += 
        }

        private void OnValidate()
        {
            nameText.text = abilityName;
        }
    }
}