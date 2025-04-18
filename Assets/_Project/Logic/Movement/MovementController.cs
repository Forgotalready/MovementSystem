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
    private readonly ControllersEventBus _eventBus;

    private bool _isCameraBlock = false;
    
    public MovementController(ControllersEventBus eventBus) => _eventBus = eventBus;

    public event Action JumpPerformed;
    
    public void Initialize()
    {
        _input = new PlayerInput();
        _input.Enable();
        _input.Gameplay.Jump.performed += OnJumpPerformed;
        _eventBus.Subscribe<UIInteraction>(OnUIInteraction);
    }

    private void OnUIInteraction(UIInteraction uiInteraction) => 
            _isCameraBlock = !_isCameraBlock;

    private void OnJumpPerformed(InputAction.CallbackContext _) => 
            JumpPerformed?.Invoke(); 

    public Vector3 ReadMove()
    {
        var input = _input.Gameplay.Move.ReadValue<Vector2>();
        return new Vector3(input.x, 0f, input.y);
    }

    public Vector2 MouseDelta
    {
        get
        {
            if (!_isCameraBlock)
            {
                return _input.Gameplay.Look.ReadValue<Vector2>();
            }
            return Vector2.zero;
        }
    }

    public void Dispose()
    {
        _input.Gameplay.Jump.performed -= OnJumpPerformed;
        _input.Disable();
        _eventBus.Unsubscribe<UIInteraction>(OnUIInteraction);
    }
}