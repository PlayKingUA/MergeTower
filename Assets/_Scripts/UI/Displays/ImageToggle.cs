using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageToggle : MonoBehaviour
{
    [SerializeField] private GameObject image;

    public void EnableImage(bool isActive)
    {
        image.SetActive(isActive);
    }
}
