using System;
using UnityEngine;

public class Jump : BaseState
{
    private Vector3 _horizontalDirection;
    private float _verticalVelocity;
    private float _gravity = 9.8f;
    private readonly DetectionComponent _detectionComponent;

    private bool _isGrounded;
    private bool _isLeftGround;

    public Jump(CharacterController characterController, MovementController movementController, Animator animator,
            PlayerSettings playerSettings,
            DetectionComponent detectionComponent) :
            base(characterController, movementController, animator, playerSettings)
    {
        _detectionComponent = detectionComponent;
    }

    public override event Action<Type> StateChange;

    public override void OnEnter()
    {
        _horizontalDirection = ToLocalCoordinates(PlayerSettings.MaxSpeed * MovementController.ReadMove());
        _verticalVelocity = Mathf.Sqrt(2 * _gravity * PlayerSettings.JumpHeight);
    }

    public override void HandleInput()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        _horizontalDirection.y = _verticalVelocity;
        CharacterController.Move(_horizontalDirection * deltaTime);
        _verticalVelocity -= _gravity * deltaTime;

        CheckState();
    }

    private void CheckState()
    {
        if (!_detectionComponent.IsJumped)
        {
            StateChange?.Invoke(typeof(Moving));
        }
    }

    public override void OnExit()
    {
    }
}