using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] ProgressBar _playerHealth;
    [SerializeField] ProgressBar _playerStamina;
    [SerializeField] ProgressBar _enemyHealth;
    [SerializeField] Text _selectedWeapon;
    [SerializeField] EndScreen _endScreen;


    public ProgressBar PlayerHealth => _playerHealth;
    public ProgressBar PlayerStamina => _playerStamina;
    public ProgressBar EnemyHealth => _enemyHealth;
    public Text SelectedWeapon => _selectedWeapon;
    public EndScreen EndScreen => _endScreen;
}