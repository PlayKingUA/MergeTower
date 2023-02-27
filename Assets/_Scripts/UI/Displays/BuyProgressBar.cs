using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyProgressBar : MonoBehaviour
{
    private ImageToggle[] _imageToggles;
    
    private void Awake()
    {
        _imageToggles = GetComponentsInChildren<ImageToggle>();
    }

    public void SetActiveToggles(int amount)
    {
        for (var i = 0; i < _imageToggles.Length; i++)
        {
            _imageToggles[i].EnableImage(i < amount);
        }
    }
}
