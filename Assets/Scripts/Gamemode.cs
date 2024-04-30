using Unity.VisualScripting;
using UnityEngine;

class Gamemode : MonoBehaviour
{
    [SerializeField] string _gamemodeName;
    [SerializeField] UIController _UIController;
    [SerializeField] PlayerCharacter _playableCharacter;
    [SerializeField] EnemyCharacter _enemyCharacter;
    private EndScreen _endScreen;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _endScreen = _UIController.EndScreen;

        _playableCharacter.CharacterController.CharacterStats.OnHealthChange += _UIController.PlayerHealth.Change;
        _playableCharacter.CharacterController.CharacterStats.OnStaminaChange += _UIController.PlayerStamina.Change;

        _playableCharacter.CharacterController.WeaponController.WeaponChanged += weapon => _UIController.SelectedWeapon.text = weapon.Name;

        _enemyCharacter.CharacterController.CharacterStats.OnHealthChange += _UIController.EnemyHealth.Change;
        
        _enemyCharacter.EnemyAI.SetTarget(_playableCharacter);
        
        _playableCharacter.CharacterController.CharacterStats.OnHealthChange += health => {
            if (health <= 0)
            {
                _endScreen.Show("Whops, you have died. Do you want try one more time?", "Definetly", "I've had enough");
                ProcessGameEnd();
            }
        };

        _enemyCharacter.CharacterController.CharacterStats.OnHealthChange += health => {
            if (health <= 0)
            {
                _endScreen.Show("Great Enemy Felled", "Gimme one more bastard to destroy", "Meh, boring game");
                ProcessGameEnd();
            }
        };
    }

    private void ProcessGameEnd()
    {
        // _playableCharacter.Disable();
        // _enemyCharacter.Disable();
    }
}
