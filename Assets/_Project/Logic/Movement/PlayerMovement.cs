using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private MovementController _movementController;

    private CharacterController _characterController;

    [Inject]
    private void Construct(MovementController movementController) =>
            _movementController = movementController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _movementController.JupmPerfomed += Jump;
    }

    private void Jump()
    {
        throw new NotImplementedException();
    }

    private void Update() =>
            _characterController.Move(_movementController.ReadMove() * (_movementSpeed * Time.deltaTime));

    private void OnDestroy()
    {
        _movementController.JupmPerfomed -= Jump;
    }
}