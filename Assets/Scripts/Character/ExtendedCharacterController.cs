using Unity.VisualScripting;
using UnityEngine;

public class ExtendedCharacterController : MonoBehaviour
{
    [SerializeField] WeaponController _weaponController;
    [SerializeField] CombatController _combatController;
    [SerializeField] CharacterStats _characterStats;

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
        inputProvider.SubscribeOnInputUpdate(Operate);
        Debug.Log($"Called sdasd for {gameObject.name}");
    }

    public virtual void Operate(InputContainer inputContainer)
    {
        if (!_canPerformNewAction) return;

        var isMoving = inputContainer.MoveDirection != Vector3.zero;
        var isAttacking = inputContainer.Attack || inputContainer.SecondaryAttack;

        _animator.SetBool("IsMoving", isMoving && !isAttacking);

        if (!_combatController.Rolling) _characterController.Move(inputContainer.MoveDirection * _speed * Time.deltaTime);

        if (inputContainer.Attack) _combatController.Attack();
        if (inputContainer.SecondaryAttack) _combatController.SecondaryAttack();
        if (inputContainer.Dodge) _combatController.Dodge();
    }

    private void Update()
    {
        ApplyGravity();
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

