using System;
using UnityEngine;

public class Falling : BaseState
{
    private Vector3 _playerDirection;
    private float _verticalVelocity;
    private float _gravity;

    private readonly DetectionComponent _detectionComponent;

    public Falling(CharacterController characterController, MovementController movementController, Animator animator,
            PlayerSettings playerSettings, EnvironmentConfig environmentConfig,
            DetectionComponent detectionComponent) : base(characterController,
            movementController, animator, playerSettings, environmentConfig)
    {
        _detectionComponent = detectionComponent;
        _gravity = environmentConfig.Gravity;
    }

    public override event Action<Type> StateChange;

    public override void OnEnter()
    {
        _playerDirection = ToLocalCoordinates(MovementController.ReadMove() * PlayerSettings.MaxSpeed);
        _verticalVelocity = 0f;
    }

    public override void HandleInput()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        _playerDirection.y = _verticalVelocity;
        CharacterController.Move(_playerDirection * deltaTime);
        _verticalVelocity -= _gravity * deltaTime;

        CheckState();
    }

    private void CheckState()
    {
        if (_detectionComponent.IsGrounded)
        {
            StateChange?.Invoke(typeof(Moving));
        }
    }

    public override void OnExit()
    {
    }
}