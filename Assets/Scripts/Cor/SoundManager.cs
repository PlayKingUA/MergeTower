using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlueStellar.Cor
{
    public class SoundManager : MonoBehaviour
    {
        #region Singelton

        public static SoundManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        #region Variables

        [Header("Sounds")]
        [SerializeField] AudioSource soundClaim;
        [SerializeField] AudioSource soundHit;

        #endregion

        public void SoundClaimActive()
        {
            soundClaim.Play();
        }

        public void SoundHitActive()
        {
            soundHit.Play();
        }
    }
}
