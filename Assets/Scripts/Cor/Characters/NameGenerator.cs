using UnityEngine;
using TMPro;

namespace BlueStellar.Cor
{
    public class NameGenerator : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textName;
        [SerializeField] string[] names;
        private string _name;

        public string Name()
        {
            return _name;
        }

        private void Start()
        {
            int randomIndex = Random.Range(0, names.Length);
            textName.text = names[randomIndex];
            _name = names[randomIndex];
        }
    }
}
