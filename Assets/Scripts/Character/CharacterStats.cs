using System;
using DG.Tweening;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 200;
    [SerializeField] private float _maxStamina = 100;
    private float _health;
    private float _stamina;
    private float _staminaTimer = 2;
    private float _staminaPerSecond = 50;
    private bool _invincibilityState;
    Tween _regenerationProcess;
    public Action<float> OnHealthChange;

    public Action<float> OnStaminaChange;

    public float Health => _health;
    public float Stamina => _stamina;
    public bool IsInvincible => _invincibilityState;

    void Awake()
    {
        _health = _maxHealth;
        _stamina = _maxStamina;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        OnHealthChange.Invoke(_health / _maxHealth);

        if (_health < 0)
        {
            _health = 0;
        }
    }

    public void SpendStamina(float stamina)
    {
        _stamina -= stamina;
        _staminaTimer = 2;
        _regenerationProcess?.Kill();
        OnStaminaChange?.Invoke(_stamina / _maxStamina);

        if (_health < 0)
        {
            _health = 0;
        }
    }

    public void RegenerateStamina()
    {
        if (_staminaTimer <= 0 && _stamina < _maxStamina)
        {
            _regenerationProcess = DOVirtual.Float(_stamina, _maxStamina, (_maxStamina - _stamina) / _staminaPerSecond, value =>
            {
                _stamina = value;
                OnStaminaChange.Invoke(_stamina / _maxStamina);
            });
            _staminaTimer = 2;
        }
        _staminaTimer -= Time.deltaTime;
    }

    public void SetInvincible(bool state)
    {
        _invincibilityState = state;
    }

    void Update()
    {
        RegenerateStamina();
    }
}