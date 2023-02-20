using UnityEngine;
using UnityEngine.UI;

namespace BlueStellar.Cor
{
    public class NameChanger : MonoBehaviour
    {
        [SerializeField] InputField inputField;
        [SerializeField] PlayerName _playerName;

        private void Start()
        {
            _playerName = GameObject.FindObjectOfType<PlayerName>();
            if(_playerName.Name() != "PLAYER")
                inputField.text = _playerName.Name();
        }

        public void ChangeName()
        {
            _playerName.NewName(inputField.text);
        }
    }
}
