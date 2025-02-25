using System;
using UnityEngine;

public class Moving : BaseState
{
    private Vector3 _moveDirection;
    private float _movementSpeed = 4f;

    public Moving(CharacterController characterController, MovementController movementController, Animator animator,
            PlayerSettings playerSettings) :
            base(characterController, movementController, animator, playerSettings)
    { }

    public override event Action<Type> StateChange;
    public override void OnEnter() => MovementController.JumpPerformed += OnJumpPerformed;

    private void OnJumpPerformed() => StateChange?.Invoke(typeof(Jump));

    public override void HandleInput()
    {
        _moveDirection = MovementController.ReadMove();
        _moveDirection.Normalize();
    }

    public override void OnUpdate(float deltaTime)
    {
        HandleInput();
        CharacterController.Move(_moveDirection * (deltaTime * _movementSpeed));
    }

    public override void OnExit() => MovementController.JumpPerformed -= OnJumpPerformed;
}