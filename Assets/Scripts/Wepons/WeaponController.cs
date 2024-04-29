using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] List<Weapon> _weaponPrefabs;
    private List<Weapon> _cachedWeapons = new();
    private Weapon _currentWeapon;
    private int _counter = 0;
    private Transform _weaponMountPoint;
    private Animator _animator;
    public Action<Weapon> WeaponChanged;
    public Weapon CurrentWeapon => _currentWeapon;

    internal void Initialize(IInputProvider playerInput, Transform weaponMountPoint, Animator animator, CharacterStats character)
    {
        _weaponMountPoint = weaponMountPoint;
        _animator = animator;
        SetupWeapons(character);
        playerInput.SubscribeOnInputUpdate(OnInput);
        ChangeWeapon();
    }

    private void OnInput(InputContainer inputContainer)
    {
        if (inputContainer.ChangeWeapon)
        {
            ChangeWeapon();
        }
    }

    private void SetupWeapons(CharacterStats character)
    {
        foreach (var weaponPrefab in _weaponPrefabs)
        {
            var weapon = Instantiate(weaponPrefab, _weaponMountPoint);
            weapon.Initialize(character);
            weapon.gameObject.SetActive(false);
            _cachedWeapons.Add(weapon);
        }
    }

    private void ChangeWeapon()
    {
        _currentWeapon?.gameObject.SetActive(false);
        _currentWeapon = _cachedWeapons[_counter % _cachedWeapons.Count];
        WeaponChanged?.Invoke(_currentWeapon);
        _animator.speed = _currentWeapon.AttackSpeed;
        _currentWeapon.gameObject.SetActive(true);

        _counter++;
    }
}
