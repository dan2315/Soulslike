using UnityEngine;

public class EnemyExtendedCharacterController : ExtendedCharacterController
{
    public override void ProcessInput(InputContainer inputContainer)
    {
        if (inputContainer.MoveDirection != Vector3.zero)
        {
            Vector3 moveDirection = Vector3.zero;

            Vector3 targetDirection = inputContainer.MoveDirection;
            moveDirection = Vector3.Lerp(moveDirection, targetDirection, 0.5f);

            inputContainer.Set(moveDirection);

        }

        base.ProcessInput(inputContainer);
    }


}