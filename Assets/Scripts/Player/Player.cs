using UnityEngine;

class Player : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] ExtendedCharacterController _characterController;
    [SerializeField] CameraController _cameraController;
    [SerializeField] Transform _visuals;
    [SerializeField] WeaponController _weaponController;
    [SerializeField] Transform _weaponMountPoint;


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _cameraController.Initialize(_playerInput);
        _characterController.Initialize(_playerInput, _visuals);
        _weaponController.Initialize(_playerInput, _weaponMountPoint);
    }
}
