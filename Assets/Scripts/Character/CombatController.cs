using System;
using DG.Tweening;
using UnityEngine;

public class CombatController : MonoBehaviour
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
    private Tween _rootMovement;
    private bool _isAttacking;
    private bool _rolling;
    private bool _staggered;
    public bool Attacking => _isAttacking;
    public bool Rolling => _rolling;
    public bool Staggered => _staggered;

    public void Initialize(CharacterController characterController, CharacterStats characterStats, Animator animator, Action OnActionStart, Action ActionPerformed, Transform visuals, WeaponController weaponController)
    {
        _characterController = characterController;
        _characterStats = characterStats;
        _animator = animator;
        _onStart = OnActionStart;
        _weaponController = weaponController;
        _animatorEvents.OnAnimationCompleted += ActionPerformed;
        _animatorEvents.OnAnimationCompleted += OnAnyAnimationCompleted;
        _characterStats.OnHealthChange += Stagger;
        _visuals = visuals;
    }
    public void PrimaryAttack()
    {
        Attack(20, "Attack");
    }

    public void SecondaryAttack()
    {
        Attack(30, "SecondaryAttack");

    }
    private void Attack(float staminaConsumed, string animationTrigger)
    {
        if (_isAttacking) return;
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Third Swing")
        || _animator.GetCurrentAnimatorStateInfo(0).IsName("Third Swing 0")) return;
        if (ProcessStamina(staminaConsumed)) return;

        _onStart.Invoke();
        _isAttacking = true;
        _animator.SetBool("Attacking", _isAttacking);
        _animator.SetTrigger(animationTrigger);
        _weaponController.CurrentWeapon.SetHitbox(true);
        _rootMovement = DOTween.Sequence()
        .AppendInterval(0.2f)
        .Append(AnimateRootMovement(0.25f, 0.5f));
    }

    public void Dodge()
    {
        if (_rolling) return;
        if (ProcessStamina(15)) return;

        _onStart.Invoke();
        _rolling = true;
        _animator.SetBool("Dodge", _rolling);
        _characterStats.SetInvincible(true);
        _rootMovement = DOTween.Sequence()
        .AppendInterval(0.2f)
        .Append(AnimateRootMovement(0.5f, 0.8f));
    }

    public void Stagger(float damage)
    {
        _onStart.Invoke();
        StopAttacking();
        _animator.SetTrigger("HitTaken");
        _staggered = true;
    }

    private void OnAnyAnimationCompleted()
    {
        StopStagger();
        StopRolling();
        StopAttacking();
    }
    private void StopStagger()
    {
        _staggered = false;
    }
    private void StopRolling()
    {
        if (_rolling)
        {
            _rolling = false;
            _animator.SetBool("Dodge", _rolling);
            _characterStats.SetInvincible(false);
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

    private void OnDestroy()
    {
        _rootMovement.Kill();
        _animatorEvents.OnAnimationCompleted -= StopRolling;
        _animatorEvents.OnAnimationCompleted -= StopAttacking;
    }
}