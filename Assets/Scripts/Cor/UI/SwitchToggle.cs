using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BlueStellar.Cor
{
    public class SwitchToggle : MonoBehaviour
    {
        [SerializeField] RectTransform uiHandleRectTransform;
        [SerializeField] Image bgImg;
        [SerializeField] Image handleImg;
        [SerializeField] Sprite onBg;
        [SerializeField] Sprite onImg;
        [SerializeField] Sprite offBg;
        [SerializeField] Sprite offImg;
        [SerializeField] Toggle toggle;
        [SerializeField] VibrationController vibrationController;

        Vector2 handlePosition;

        private void Start()
        {
            handlePosition = uiHandleRectTransform.anchoredPosition;

            toggle.onValueChanged.AddListener(OnSwitch);

            if (!vibrationController.ISOffVibration())
                toggle.isOn = true;
            if (toggle.isOn)
                OnSwitch(true);
        }

        private void OnSwitch(bool on)
        {
            uiHandleRectTransform.DOAnchorPos(on ? handlePosition * -0.1f : handlePosition, .4f).SetEase(Ease.InOutBack);

            if (on)
            {
                bgImg.sprite = onBg;
                handleImg.sprite = onImg;
                vibrationController.VibrationOffAndOn(false);
                return;
            }

            bgImg.sprite = offBg;
            handleImg.sprite = offImg;
            vibrationController.VibrationOffAndOn(true);
        }
    }
}
