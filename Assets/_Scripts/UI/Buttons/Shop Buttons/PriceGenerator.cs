using UnityEngine;

namespace _Scripts.UI.Buttons.Shop_Buttons
{
    [System.Serializable]
    public class PriceGenerator
    {
        [SerializeField] protected int startPrice;
        [SerializeField] protected int baseAmount;
        [SerializeField] protected float multiplier;
        [SerializeField] protected float powMultiplier;
        
        public int GetPrise(int level)
        {
            if (level == 0)
            {
                return startPrice;
            }

            return GetPrise(level - 1) + 
                   (int) (multiplier * baseAmount * Mathf.Pow(level + 1,powMultiplier));
        }
    }
}