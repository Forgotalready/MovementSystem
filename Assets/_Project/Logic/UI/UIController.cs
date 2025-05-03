using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : IGameStartListener, IGameFinishListener
{
    private readonly ControllersEventBus _controllersEventBus;
    
    private readonly UIInput _uiInput = new();
    public event Action<Vector2> ClickPerformed;
    public event Action InventoryOpened;
    
    public UIController(ControllersEventBus controllersEventBus) => 
            _controllersEventBus = controllersEventBus;
    
    public void OnGameStart()
    {
        _uiInput.Enable();
        _uiInput.UI.Click.performed += OnClick;
        _uiInput.UI.OpenInventory.performed += OnInventoryOpen;
    }

    private void OnInventoryOpen(InputAction.CallbackContext obj)
    {
        InventoryOpened?.Invoke();
        _controllersEventBus.Publish(new UIInteraction());
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnClick(InputAction.CallbackContext obj) => 
            ClickPerformed?.Invoke(Mouse.current.position.ReadValue());

    public void OnGameFinish()
    {
        _uiInput.UI.Click.performed -= OnClick;
        _uiInput.UI.OpenInventory.performed -= OnInventoryOpen;
        _uiInput.Disable();
    }
}