using _Scripts.Game_States;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace _Scripts.Cameras
{
    public class CameraManager : MonoBehaviour
    {
        #region Variables
        [Inject] private GameStateManager _gameStateManager;
        [SerializeField] private CinemachineVirtualCamera[] cameras;
        [SerializeField] private CameraType currentCameraType;

        private CinemachineVirtualCamera _currentCamera;
        #endregion

        private void Start()
        {
            _gameStateManager.AttackStarted += () =>
            {
                ChangeCamera(CameraType.Attack);
            };
        }

        #region Change Camera
        public void ChangeCamera(CameraType cameraType)
        {
            currentCameraType = cameraType;

            foreach (var virtualCamera in cameras)
            {
                virtualCamera.m_Priority = 0;
            }
            _currentCamera = cameras[(int)currentCameraType];
            _currentCamera.m_Priority = 100;
        }

        public void SetCamera(CameraType cameraType, CinemachineVirtualCamera targetCamera)
        {
            targetCamera.m_Priority = cameras[(int) cameraType].m_Priority;
            cameras[(int) cameraType].m_Priority = 0;
            cameras[(int) cameraType] = targetCamera;

            if (currentCameraType == cameraType)
            {
                _currentCamera = targetCamera;
            }
        }
        #endregion`
    }
}
