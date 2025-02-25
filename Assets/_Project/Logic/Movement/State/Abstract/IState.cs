using System;

public interface IState
{
    public event Action<Type> StateChange;
    
    public void OnEnter();
    public void HandleInput();
    public void OnUpdate(float deltaTime);
    public void OnExit();
}
