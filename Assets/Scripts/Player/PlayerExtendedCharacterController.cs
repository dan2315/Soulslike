using UnityEngine;

class PlayerExtendedCharacterController : ExtendedCharacterController
{
    [SerializeField] private CameraController _cameraController;

    public override void Operate(InputContainer inputContainer)
    {
        if (inputContainer.MoveDirection != Vector3.zero)
        {
            Vector3 moveDirection = Vector3.zero;

            float cameraDirection = -_cameraController.CameraLookAngle * Mathf.Deg2Rad;

            Vector3 newForward = new(Mathf.Cos(cameraDirection), 0, Mathf.Sin(cameraDirection));
            Vector3 newRight = new(Mathf.Cos(cameraDirection + 90 * Mathf.Deg2Rad), 0, Mathf.Sin(cameraDirection + 90 * Mathf.Deg2Rad));

            Debug.DrawRay(transform.position, newForward);
            Debug.DrawRay(transform.position, newRight, Color.green);

            Vector3 targetDirection = newRight * inputContainer.MoveDirection.x - newForward * inputContainer.MoveDirection.z;
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