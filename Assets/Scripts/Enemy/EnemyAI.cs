using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour, IInputProvider
{
    [SerializeField] private float _attackRange = 2.0f;
    [SerializeField] private float _dodgeChance = 0.25f;
    private Character _target;
    private InputContainer _inputContainer = new();

    private Action<InputContainer> _onDecisionMade;

    public void SubscribeOnInputUpdate(Action<InputContainer> action)
    {
        _onDecisionMade += action;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _target.transform.position);
        DecideAction(distanceToPlayer);
        _onDecisionMade.Invoke(_inputContainer);
    }

    void DecideAction(float distanceToPlayer)
    {
        _inputContainer.Reset();
        Vector3 moveDirection = Vector3.zero;
        bool secondaryAttack = false;
        bool dodge = false;
        bool attack = false;

        if (distanceToPlayer > _attackRange)
        {
            moveDirection = (_target.transform.position - transform.position).normalized;
        }
        else
        {
            if (Random.value < _dodgeChance)
            {
                dodge = true;
            }
            else
            {
                if (Random.value > 0.5f) attack = true;
                else secondaryAttack = false;
            }
        }

        _inputContainer.Set(moveDirection, attack, secondaryAttack, dodge);
    }

    public void SetTarget(Character player)
    {
        _target = player;
    }
}
