using UnityEngine;

public class LookAtTheCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted
    }

    [SerializeField] private Mode mode;
    [SerializeField] private Transform targetCamera;

    private void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(targetCamera.transform);
                break;
            case Mode.LookAtInverted:
                var directionFromCamera = transform.position - targetCamera.position;
                transform.LookAt(directionFromCamera + directionFromCamera);
                break;
        }
    }
}
