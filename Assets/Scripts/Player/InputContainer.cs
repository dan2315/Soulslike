using UnityEngine;

public class InputContainer
{
    private Vector3 _moveDirection;
    private Vector2 _cameraMoveDirection;

    private bool _attack;
    private bool _secondaryAttack;
    private bool _dodge;
    private bool _changeWeapon;


    public Vector3 MoveDirection => _moveDirection;
    public Vector3 CameraMoveDirection => new Vector3(-_cameraMoveDirection.y, _cameraMoveDirection.x);
    public bool Attack => _attack;
    public bool SecondaryAttack => _secondaryAttack;
    public bool Dodge => _dodge;
    public bool ChangeWeapon => _changeWeapon;


    public void Set(Vector3 moveDirection, Vector2 mouseMoveDirection, bool attack, bool secondaryAttack, bool dodge, bool changeWeapon)
    {
        _moveDirection = moveDirection;
        _cameraMoveDirection = mouseMoveDirection;
        _attack = attack;
        _secondaryAttack = secondaryAttack;
        _dodge = dodge;
        _changeWeapon = changeWeapon;
    }
    public void Set(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;
    }
}