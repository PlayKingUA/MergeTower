using _Scripts.Cameras;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using CameraType = _Scripts.Cameras.CameraType;

namespace _Scripts.UI.Windows
{
    public class MenuWindow : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] private Button openAbilitiesButton;
        [SerializeField] private Button closeAbilitiesButton;
        
        [Space(10)]
        [SerializeField] private CanvasGroup upgradePanel;
        [SerializeField] private CanvasGroup abilitiesPanel;

        [Inject] private CameraManager _cameraManager;
        
        private void Awake()
        {
            openAbilitiesButton.onClick.AddListener(() =>
            {
                ShopWindowSwipe(true);
                _cameraManager.ChangeCamera(CameraType.Abilities);
            });
            closeAbilitiesButton.onClick.AddListener(() =>
            {
                ShopWindowSwipe(false);
                _cameraManager.ChangeCamera(CameraType.Menu);
            });
            closeAbilitiesButton.onClick.Invoke();
        }
        
        private void ShopWindowSwipe(bool isAbilitiesPanel)
        {
            WindowsManager.CanvasGroupSwap(abilitiesPanel, isAbilitiesPanel);
            WindowsManager.CanvasGroupSwap(upgradePanel, !isAbilitiesPanel);
        }
    }
}