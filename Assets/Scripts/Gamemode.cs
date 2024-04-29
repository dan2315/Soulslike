using UnityEngine;

class Gamemode : MonoBehaviour
{
    [SerializeField] string _gamemodeName;
    [SerializeField] UIController _UIController;
    [SerializeField] PlayerExtendedCharacterController _playableCharacter;
    [SerializeField] EnemyExtendedCharacterController _enemyCharacter;

    public void Awake()
    {
        _playableCharacter.CharacterStats.OnHealthChange += _UIController.PlayerHealth.Change;
        _playableCharacter.CharacterStats.OnStaminaChange += _UIController.PlayerStamina.Change;

        _playableCharacter.WeaponController.WeaponChanged += weapon => _UIController.SelectedWeapon.text = weapon.Name;

        // _enemyCharacter.CharacterStats.OnHealthChange += _UIController.EnemyHealth.Change;
    }
}
