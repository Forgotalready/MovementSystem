using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

/// <summary>
/// Класс для работы с системой ввода Unity
/// </summary>
public class MovementController : IInitializable, IDisposable
{
    private PlayerInput _input;

    public event Action JupmPerfomed;
    
    public void Initialize()
    {
        _input = new PlayerInput();
        _input.Enable();
        _input.Gameplay.Jump.performed += OnJumpPerformed;
    }

    private void OnJumpPerformed(InputAction.CallbackContext _) => JupmPerfomed?.Invoke(); 

    public Vector3 ReadMove()
    {
        var input = _input.Gameplay.Move.ReadValue<Vector2>();
        return new Vector3(input.x, 0f, input.y);
    }

    public void Dispose()
    {
        _input.Disable();
    }
}