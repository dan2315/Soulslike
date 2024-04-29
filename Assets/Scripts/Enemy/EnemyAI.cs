using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IInputProvider
{
    public void SubscribeOnInputUpdate(Action<InputContainer> action)
    {
        throw new NotImplementedException();
    }
}
