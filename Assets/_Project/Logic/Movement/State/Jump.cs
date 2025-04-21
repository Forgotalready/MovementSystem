using System;
using UnityEditor.Build;
using UnityEngine;

public class Jump : BaseState
{
    private Vector3 _horizontalDirection;
    private float _verticalVelocity;
    private float _gravity;
    private readonly DetectionComponent _detectionComponent;

    private bool _isGrounded;
    private bool _isLeftGround;
    private float _jumpHeight; // Если над игроком "открытое" небо, то равно высоте прыжка, иначе, высоте до препятствия

    public Jump(CharacterController characterController, MovementController movementController, Animator animator,
            PlayerConfig playerConfig, EnvironmentConfig environmentConfig,
            DetectionComponent detectionComponent) :
            base(characterController, movementController, animator, playerConfig, environmentConfig)
    {
        _detectionComponent = detectionComponent;
        _gravity = environmentConfig.Gravity;
    }

    public override event Action<Type> StateChange;

    public override void OnEnter()
    {
        _horizontalDirection = ToLocalCoordinates(PlayerConfig.MaxSpeed * MovementController.ReadMove());
        _jumpHeight = CalculateJumpHeight();

        _verticalVelocity = ChooseVerticalVelocity();
    }

    private float ChooseVerticalVelocity()
    {
        if (Mathf.Approximately(_gravity, 0f))
        {
            return PlayerConfig.DefaultVerticalVelocity;
        }
        return Mathf.Sqrt(2 * _gravity * _jumpHeight);
    }

    private float CalculateJumpHeight()
    {
        Vector3 playerUpDirection = CharacterController.gameObject.transform.up;
        Vector3 playerPosition = CharacterController.transform.position;
        Vector3 topPointPosition = new Vector3(playerPosition.x, playerPosition.y + CharacterController.height / 2f,
                playerPosition.z);
        if (Physics.Raycast(topPointPosition, playerUpDirection, out RaycastHit hit, PlayerConfig.JumpHeight))
        {
            return hit.distance;
        }

        return PlayerConfig.JumpHeight;
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