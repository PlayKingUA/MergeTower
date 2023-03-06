using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Weapons
{
    public class WeaponAnimator : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        private Animator _animator;

        private readonly int _idleHash = Animator.StringToHash("Idle");
        private readonly int _attackHash = Animator.StringToHash("Attack");
        private readonly int _deathHash = Animator.StringToHash("Death");

        private int _currentState;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SetAnimation(SoldierState state)
        {
            if (_currentState == _deathHash)
                return;
            
            _currentState = GetHash(state);
            if (_animator)
                _animator.Play(_currentState, 0, 0);
        }

        private int GetHash(SoldierState state)
        {
            var hash = state switch
            {
                SoldierState.Idle => _idleHash,
                SoldierState.Attack => _attackHash,
                SoldierState.Death => _deathHash,
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
            return hash;
        }
    }
}