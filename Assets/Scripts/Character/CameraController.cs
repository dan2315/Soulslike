using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _cameraRotationPivot;
    
    public float CameraLookAngle => _cameraRotationPivot.localEulerAngles.y;

    public void Initialize(IInputProvider playerInput)
    {
        playerInput.SubscribeOnInputUpdate(UpdateCameraTransform);
    }

    private void UpdateCameraTransform(InputContainer inputContainer)
    {
        Vector3 targetEulers = _cameraRotationPivot.eulerAngles + inputContainer.CameraMoveDirection;
        targetEulers.x = NormalizeAngle(targetEulers.x);
        targetEulers.x = Mathf.Clamp(targetEulers.x, -60f, 60f);
        _cameraRotationPivot.eulerAngles = targetEulers;

        float NormalizeAngle(float angle)
        {
            while (angle > 180) angle -= 360;
            while (angle < -180) angle += 360;
            return angle;
        }
    }
}