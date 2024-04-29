using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

class CombatController : MonoBehaviour
{
    [SerializeField] Transform _hip;
    [SerializeField] AnimatorEvents _animatorEvents;
    private CharacterController _characterController;
    private CharacterStats _characterStats;
    private Transform _visuals;
    private Animator _animator;
    private WeaponController _weaponController;
    private Vector3 _previousHipPosition;
    private Action _onStart;
    private bool _isAttacking;
    private bool _rolling;
    public bool Rolling => _rolling;

    public void Initialize(CharacterController characterController, CharacterStats characterStats, Animator animator, Action OnActionStart, Action ActionPerformed, Transform visuals, WeaponController weaponController)
    {
        _characterController = characterController;
        _characterStats = characterStats;
        _animator = animator;
        _onStart = OnActionStart;
        _weaponController = weaponController;
        _animatorEvents.OnAnimationCompleted += ActionPerformed;
        _animatorEvents.OnAnimationCompleted += StopRolling;
        _animatorEvents.OnAnimationCompleted += StopAttacking;
        _visuals = visuals;
    }
    public void Attack()
    {
        if (_isAttacking == false)
            if (ProcessStamina(20)) return;

        _onStart.Invoke();
        _isAttacking = true;
        _animator.SetBool("Attacking", _isAttacking);
        _animator.SetTrigger("Attack");
        _weaponController.CurrentWeapon.SetHitbox(true);
        DOTween.Sequence()
        .AppendInterval(0.2f)
        .Append(AnimateRootMovement(0.25f, 0.5f));
    }

    public void SecondaryAttack()
    {
        if (_isAttacking == false)
            if (ProcessStamina(30)) return;

        _onStart.Invoke();
        _isAttacking = true;
        _animator.SetBool("Attacking", _isAttacking);
        _animator.SetTrigger("SecondaryAttack");
        _weaponController.CurrentWeapon.SetHitbox(true);
        DOTween.Sequence()
        .AppendInterval(0.2f)
        .Append(AnimateRootMovement(0.25f, 0.5f));
    }
    public void Dodge()
    {
        if (_rolling == false)
            if (ProcessStamina(15)) return;

        _onStart.Invoke();
        _rolling = true;
        _animator.SetBool("Dodge", _rolling);
        DOTween.Sequence()
        .AppendInterval(0.2f)
        .Append(AnimateRootMovement(0.5f, 0.8f));
    }

    private void StopRolling()
    {
        if (_rolling)
        {
            _rolling = false;
            _animator.SetBool("Dodge", _rolling);
        }
    }

    public void StopAttacking()
    {
        if (_isAttacking)
        {
            _isAttacking = false;
        _weaponController.CurrentWeapon.SetHitbox(false);
            _animator.SetBool("Attacking", _isAttacking);
        }
    }

    private bool ProcessStamina(float spentAmount)
    {
        if (_characterStats.Stamina <= 0) return true;
        _characterStats.SpendStamina(spentAmount);
        return false;
    }

    private Tween AnimateRootMovement(float initialSpeed, float duration)
    {
        float speed = initialSpeed;
        return DOVirtual.Float(speed, 0, duration, speed =>
        {
            _characterController.Move(_visuals.forward * speed);
        }).SetEase(Ease.OutSine).SetUpdate(UpdateType.Normal);
    }

    private void LateUpdate()
    {
        Vector3 currentPosition = _hip.localPosition;
        currentPosition.x = _previousHipPosition.x;
        currentPosition.z = _previousHipPosition.z;
        _hip.localPosition = currentPosition;

        _previousHipPosition = _hip.localPosition;
    }
}