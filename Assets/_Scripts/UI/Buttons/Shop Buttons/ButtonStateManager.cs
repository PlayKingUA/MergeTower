using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    public class ButtonStateManager : MonoBehaviour
    {
        [ShowInInspector] private ButtonBuyState _buttonState;
        [SerializeField] protected GameObject[] states;
        [Space(10)]
        [SerializeField] private GameObject unlockedState;

        public ButtonBuyState ButtonState => _buttonState;

        public void SetUIState(ButtonBuyState targetState)
        {
            _buttonState = targetState;
            foreach (var state in states)
            {
                state.SetActive(false);
            }

            if (unlockedState != null)
            {
                unlockedState.SetActive(_buttonState != ButtonBuyState.Locked);
            }

            if (_buttonState == ButtonBuyState.BuyWithADs)
            {
                states[(int)ButtonBuyState.BuyWithMoney].SetActive(true);
            }
            else
            {
                states[(int)_buttonState].SetActive(true);
            }
        }
    }
}