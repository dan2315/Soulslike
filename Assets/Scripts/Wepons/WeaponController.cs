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

    public Action<Weapon> WeaponChanged;

    internal void Initialize(PlayerInput playerInput, Transform weaponMountPoint)
    {
        _weaponMountPoint = weaponMountPoint;
        SetupWeapons();
        playerInput.SubscribeOnInputUpdate(OnInput);
    }

    private void OnInput(InputContainer inputContainer)
    {
        if (inputContainer.ChangeWeapon)
        {
            ChangeWeapon();
        }
    }

    private void SetupWeapons()
    {
        foreach (var weaponPrefab in _weaponPrefabs)
        {
            var weapon = Instantiate(weaponPrefab, _weaponMountPoint);
            weapon.gameObject.SetActive(false);
            _cachedWeapons.Add(weapon);
        }
    }

    private void ChangeWeapon()
    {
        _currentWeapon?.gameObject.SetActive(false);
        _currentWeapon = _cachedWeapons[_counter % _cachedWeapons.Count];
        WeaponChanged?.Invoke(_currentWeapon);
        _currentWeapon.gameObject.SetActive(true);

        _counter++;
    }
}
