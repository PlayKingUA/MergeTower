using System;
using _Scripts.Game_States;
using _Scripts.Slot_Logic;
using _Scripts.Tower_Logic;
using _Scripts.UI.Upgrade;
using _Scripts.Units;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Scripts.Weapons
{
    public class Weapon : AttackingObject
    {
        #region Variables
        [Space(10)] 
        [SerializeField] private GameObject appearFx;
        [SerializeField] private GameObject destroyFx;
        [SerializeField] private Transform gunTransform;
        [Space(10)]
        [ShowInInspector, ReadOnly] private SoldierState _currentState;
        [ShowInInspector, ReadOnly] private int _level;

        [Inject] private GameStateManager _gameStateManager;
        [Inject] protected AttackZone AttackZone;
        [Inject] private UpgradeMenu _upgradeMenu;
        [Inject] private SpeedUpLogic _speedUpLogic;

        protected WeaponAnimator WeaponAnimator;
        private Material _gunMaterial;
        private Tweener _shakeTween;

        private const float MaxShakeStrength = 0.05f;
        private const float DestroyColorChangeDuration = 0.35f;
        
        [ShowInInspector, ReadOnly] private protected Zombie TargetZombie;
        #endregion

        #region Properties
        public int Level => _level;
        public GameObject AppearFx => appearFx;

        private protected bool CanAttack => AttackTimer >= CoolDown  && TargetZombie != null;

        protected override float CoolDown => base.CoolDown / _speedUpLogic.CoolDownSpeedUp;
        #endregion
        
        #region Monobehaviour Callbacks
        protected override void Start()
        {
            base.Start();
            WeaponAnimator = GetComponent<WeaponAnimator>();
            
            ChangeState(SoldierState.Idle);
            
            //_speedUpLogic.OnTapCountChanged += Shake;

            _gameStateManager.Fail += DestroyWeapon;
        }

        protected override void Update()
        {
            base.Update();
            UpdateState();
        }

        private void OnDisable()
        {
            //_speedUpLogic.OnTapCountChanged -= Shake;
            _shakeTween.Kill();
        }

        #endregion
        
        #region States Logic
        public void ChangeState(SoldierState newState)
        {
            _currentState = newState;
        }
        
        private void UpdateState()
        {
            switch (_currentState)
            {
                case SoldierState.Idle:
                    IdleState();
                    break;
                case SoldierState.Attack:
                    AttackState();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void IdleState()
        {
            WeaponAnimator.SetAnimation(SoldierState.Idle);
        }

        protected virtual void AttackState()
        {
            Rotate();
        }
        #endregion

        public void SetLevel(int level)
        {
            _level = level;
        }

        #region Motion
        public void ReturnToPreviousPos(Slot previousSlot)
        {
            previousSlot.Refresh(this, previousSlot);
        }

        private void Rotate()
        {
            UpdateTargetZombie();
            
            if (TargetZombie == null)
                return;
            
            var direction = TargetZombie.ShootPoint.position - gunTransform.position;
            var rotateY = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var targetRotation = Quaternion.Euler(0, rotateY, 0);

            if (Quaternion.Angle(gunTransform.rotation, targetRotation) == 0) return;
            var t =  Mathf.Clamp(Time.deltaTime * 10, 0f, 0.99f);
            gunTransform.rotation = Quaternion.Lerp(gunTransform.rotation,
                targetRotation, t);
        }

        private void Shake()
        {
            var targetColor = new Color(_speedUpLogic.EffectPower, 0, 0);
            _gunMaterial.SetColor("_EmissionColor", targetColor);
            _gunMaterial.EnableKeyword("_EmissionColor");
            
            _shakeTween.Rewind();
            _shakeTween.Kill();
            if (_speedUpLogic.EffectPower != 0)
            {
                _shakeTween = transform.
                    DOShakePosition(_speedUpLogic.EffectDuration, _speedUpLogic.EffectPower * MaxShakeStrength)
                    .SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
            }
        }
        #endregion

        private void DestroyWeapon()
        {
            WeaponAnimator.SetAnimation(SoldierState.Death);
            if (destroyFx == null)
                return;
            /*destroyFx.SetActive(true);
            gunRenderer.material.DOColor(DestoyredColor, _destoryColorChangeDuration);*/
        }
        
        private void UpdateTargetZombie()
        {
            TargetZombie = AttackZone.GetNearestZombie(transform);
        }
    }
}