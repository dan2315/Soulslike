using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IInputProvider
{
    private Action<InputContainer> _onPlayerInput;
    private InputContainer _inputContainer = new();


    void Update()
    {
        UpdateInputContainer();
        _onPlayerInput.Invoke(_inputContainer);
    }

    public void SubscribeOnInputUpdate(Action<InputContainer> action)
    {
        _onPlayerInput += action;
    }

    public void Unsubscribe(Action<InputContainer> action)
    {
        _onPlayerInput -= action;
    }

    private void UpdateInputContainer()
    {
        float y = Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");
        Vector3 moveDirection = new(y, 0, x);

        Vector2 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        bool attack = Input.GetKey(KeyCode.Mouse0);
        bool secondaryAttack = Input.GetKey(KeyCode.Mouse1);
        bool dodge = Input.GetKey(KeyCode.LeftShift);
        bool changeWeapon = Input.GetKeyDown(KeyCode.Q);

        _inputContainer.Set(moveDirection, mouseDelta, attack, secondaryAttack, dodge, changeWeapon);
    }
}
