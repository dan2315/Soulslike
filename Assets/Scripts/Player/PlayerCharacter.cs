using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] protected PlayerInput _playerInput;
    [SerializeField] CameraController _cameraController;

    protected override void Start()
    {
        _inputProvider = _playerInput;
        _cameraController.Initialize(_inputProvider);
        base.Start();
    }
}
