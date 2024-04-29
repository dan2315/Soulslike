using System;

public interface IInputProvider
{
    public void SubscribeOnInputUpdate(Action<InputContainer> action);
}