using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class UIController : IInitializable, IDisposable
{
    private readonly DefaultInputActions _defaultInputActions = new();
    public event Action<Vector2> ClickPerformed;
    
    public void Initialize()
    {
        _defaultInputActions.Enable();
        _defaultInputActions.UI.Click.performed += OnClick;
    }

    private void OnClick(InputAction.CallbackContext obj) => ClickPerformed?.Invoke(Mouse.current.position.ReadValue());

    public void Dispose()
    {
        _defaultInputActions.UI.Click.performed -= OnClick;
        _defaultInputActions.Disable();
    }
}