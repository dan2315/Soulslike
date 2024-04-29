using UnityEngine;

public class EnemyExtendedCharacterController : ExtendedCharacterController
{
    public override void Operate(InputContainer inputContainer)
    {
        if (inputContainer.MoveDirection != Vector3.zero)
        {
            Vector3 moveDirection = Vector3.zero;

            Vector3 targetDirection = inputContainer.MoveDirection;
            moveDirection = Vector3.Lerp(moveDirection, targetDirection, 0.5f);

            inputContainer.Set(moveDirection);

            Vector3 newRotation = _visuals.transform.eulerAngles;
            newRotation.y = Vector3.SignedAngle(Vector3.forward, moveDirection, Vector3.up);
            _visuals.transform.rotation = Quaternion.Lerp(_visuals.transform.rotation, Quaternion.Euler(newRotation), 0.5f);
            if (inputContainer.Dodge) _visuals.transform.eulerAngles = newRotation;
        }

        base.Operate(inputContainer);
    }
}