using Unity.VisualScripting;
using UnityEngine;

public class ExtendedCharacterController : MonoBehaviour
{
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] protected CombatController _combatController;
    [SerializeField] private CharacterStats _characterStats;

    [Header("Options")]
    [SerializeField] private float _speed;
    [SerializeField] private float gravity = -9.81f;

    private Vector3 _velocity;
    private CharacterController _characterController;
    Animator _animator;
    private bool _canPerformNewAction = true;
    protected Transform _visuals;

    public CharacterStats CharacterStats => _characterStats;
    public WeaponController WeaponController => _weaponController;

    public void Initialize(IInputProvider inputProvider, Transform visuals, Animator animator)
    {
        _characterController = transform.AddComponent<CharacterController>();
        _characterController.center = Vector3.up * 1.085f;
        _visuals = visuals;
        _animator = animator;
        _combatController.Initialize(_characterController, _characterStats, _animator, ActionStarted, ActionPerformed, _visuals, _weaponController);
        inputProvider.SubscribeOnInputUpdate(ProcessInput);
    }

    public virtual void ProcessInput(InputContainer inputContainer)
    {
        if (!_canPerformNewAction) return;

        var isMoving = inputContainer.MoveDirection != Vector3.zero;
        var isAttacking = inputContainer.Attack || inputContainer.SecondaryAttack;
        var canMove = !isAttacking && !_combatController.Staggered && !_combatController.Rolling;

        _animator.SetBool("IsMoving", isMoving && canMove);

        if (canMove) _characterController.Move(inputContainer.MoveDirection * _speed * Time.deltaTime);
        if (isMoving && canMove) ApplyRotation(inputContainer);

        if (inputContainer.Attack) _combatController.PrimaryAttack();
        if (inputContainer.SecondaryAttack) _combatController.SecondaryAttack();
        if (inputContainer.Dodge) _combatController.Dodge();
    }

    private void Update()
    {
        ApplyGravity();
    }

    private void ApplyRotation(InputContainer inputContainer)
    {
        Vector3 newRotation = _visuals.transform.eulerAngles;
        newRotation.y = Vector3.SignedAngle(Vector3.forward, inputContainer.MoveDirection, Vector3.up);
        _visuals.transform.rotation = Quaternion.Lerp(_visuals.transform.rotation, Quaternion.Euler(newRotation), 0.5f);
        if (inputContainer.Dodge) _visuals.transform.eulerAngles = newRotation;
    }

    private void ApplyGravity()
    {
        bool isGrounded = _characterController.isGrounded;
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void ActionStarted()
    {
        _canPerformNewAction = false;
    }
    private void ActionPerformed()
    {
        _canPerformNewAction = true;
    }
}

