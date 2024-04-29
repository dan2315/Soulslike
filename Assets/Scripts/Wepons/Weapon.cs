using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] float _damage;
    [SerializeField] float _attackSpeed;
    public string Name => _name;
    public float AttackSpeed => _attackSpeed;


    bool _active = false;
    CharacterStats _me; 
    List<CharacterStats> _hitObjects = new();

    public void Initialize(CharacterStats character)
    {
        _me = character;
    }

    public void SetHitbox(bool active)
    {
        _active = active;
        if (!_active) _hitObjects = new();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterStats character))
        {
            if (!_hitObjects.Contains(character) && character != _me && _active)
            {
                character.TakeDamage(_damage);
                _hitObjects.Add(character);
            }
        }
    }
}