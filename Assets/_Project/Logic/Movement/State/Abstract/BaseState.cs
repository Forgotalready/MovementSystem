using System;
using UnityEngine;

public abstract class BaseState : IState
{
    protected CharacterController CharacterController;
    protected MovementController MovementController;
    protected Animator Animator;
    protected PlayerSettings PlayerSettings;

    protected BaseState(CharacterController characterController, MovementController movementController, Animator animator, PlayerSettings playerSettings)
    {
        CharacterController = characterController;
        MovementController = movementController;
        Animator = animator;
        PlayerSettings = playerSettings;
    }

    public abstract event Action<Type> StateChange;
    
    public abstract void OnEnter();

    public abstract void HandleInput();

    public abstract void OnUpdate(float deltaTime);

    public abstract void OnExit();
}