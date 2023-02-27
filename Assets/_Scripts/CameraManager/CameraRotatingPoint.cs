using System;
using _Scripts.Game_States;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class CameraRotatingPoint : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    
    [Inject] private GameStateManager _gameStateManager;
    private Tween _rotationTween;

    private void Start()
    {
        _gameStateManager.AttackStarted += () =>
        {
            RotateCamera(true);
        };
        _gameStateManager.Fail += () =>
        {
            RotateCamera(false);
        };
        _gameStateManager.Victory += () =>
        {
            RotateCamera(false);
        };
        
    }

    [Button("Update Speed")]
    private void UpdateSpeed()
    {
        _rotationTween.Kill();
        RotateCamera(true);
    }

    private void RotateCamera(bool isRotating)
    {
        if (!isRotating)
        {
            _rotationTween.Kill();
            return;
        }

        var sign = rotationSpeed >= 0 ? 1 : -1;
        _rotationTween = transform
            .DOLocalRotate(new Vector3(0, sign * 360, 0), Mathf.Abs(rotationSpeed), RotateMode.FastBeyond360)
            .SetLoops(-1).SetEase(Ease.Linear).SetSpeedBased();
    }
}
