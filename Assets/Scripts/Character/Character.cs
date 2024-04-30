using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private ExtendedCharacterController _characterController;
    [SerializeField] private Transform _visuals;
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private Transform _weaponMountPoint;
    [SerializeField] Animator _animator;

    protected IInputProvider _inputProvider;

    public ExtendedCharacterController CharacterController => _characterController;

    protected virtual void Start()
    {
        _characterController.Initialize(_inputProvider, _visuals, _animator);
        _weaponController.Initialize(_inputProvider, _weaponMountPoint, _animator, _characterController.CharacterStats);
    }

    public virtual void Disable()
    {
        // _visuals.gameObject.SetActive(false);
    }
}
