using UnityEngine;
using Zenject;

/// <summary>
/// Класс для отслеживания параметров, двигается ли он, на земле, прыгнул.
/// </summary>
public class DetectionComponent : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private LayerMask _groundLayerMask;
    private MovementController _movementController;

    public bool IsJumped { get; private set; } = false;
    public bool IsLeftGround { get; private set; } = false;
    public bool IsMoving { get; private set; } = false;
    public bool IsGrounded { get; private set; } = true;

    [Inject]
    private void Construct(MovementController movementController) =>
            _movementController = movementController;

    private void Start() =>
            _movementController.JumpPerformed += OnJumpPerformed;

    private void Update()
    {
        IsMoving = (_movementController.ReadMove().magnitude != 0);
        IsGrounded = Physics.CheckSphere(_groundCheckTransform.position, 0.1f, _groundLayerMask);

        if (IsJumped && !IsGrounded)
        {
            IsLeftGround = true;
        }

        if (IsLeftGround && IsGrounded && IsJumped)
        {
            IsJumped = false;
            IsLeftGround = false;
        }
    }

    private void OnJumpPerformed() =>
            IsJumped = true;

    private void OnDestroy() =>
            _movementController.JumpPerformed -= OnJumpPerformed;
}