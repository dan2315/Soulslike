using UnityEngine;

class EnemyCharacter : Character
{
    [SerializeField] protected EnemyAI _AiInput;

    public EnemyAI EnemyAI => _AiInput;

    protected override void Start()
    {
        _inputProvider = _AiInput;

        base.Start();
    }
}
