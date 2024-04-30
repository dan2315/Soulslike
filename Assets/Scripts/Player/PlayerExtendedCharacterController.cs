using UnityEngine;

class PlayerExtendedCharacterController : ExtendedCharacterController
{
    [SerializeField] private CameraController _cameraController;

    public override void ProcessInput(InputContainer inputContainer)
    {
        if (inputContainer.MoveDirection != Vector3.zero)
        {
            Vector3 moveDirection = Vector3.zero;
            CalculateCameraBasis(_cameraController, out Vector3 newForward, out Vector3 newRight);

            moveDirection = ModifyMoveDirectionWithCameraBasis(inputContainer, moveDirection, newForward, newRight);

            inputContainer.Set(moveDirection);
        }

        base.ProcessInput(inputContainer);
    }



    private static Vector3 ModifyMoveDirectionWithCameraBasis(InputContainer inputContainer, Vector3 moveDirection, Vector3 newForward, Vector3 newRight)
    {
        Vector3 targetDirection = newRight * inputContainer.MoveDirection.x - newForward * inputContainer.MoveDirection.z;
        moveDirection = Vector3.Lerp(moveDirection, targetDirection, 0.5f);
        return moveDirection;
    }

    private void CalculateCameraBasis(CameraController cameraController ,out Vector3 newForward, out Vector3 newRight)
    {
        float cameraDirection = -cameraController.CameraLookAngle * Mathf.Deg2Rad;

        newForward = new(Mathf.Cos(cameraDirection), 0, Mathf.Sin(cameraDirection));
        newRight = new(Mathf.Cos(cameraDirection + (90 * Mathf.Deg2Rad)), 0, Mathf.Sin(cameraDirection + (90 * Mathf.Deg2Rad)));
        Debug.DrawRay(transform.position, newForward);
        Debug.DrawRay(transform.position, newRight, Color.green);
    }
}