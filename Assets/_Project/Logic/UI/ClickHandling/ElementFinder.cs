using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ElementFinder : IInitializable, IDisposable
{
    private readonly UIController _uiController;
    private EventSystem _eventSystem;

    public ElementFinder(UIController uiController) => 
            _uiController = uiController;

    public void Initialize()
    {
        _uiController.ClickPerformed += HandleClickAtPosition;
        _eventSystem = EventSystem.current;
    }

    private void HandleClickAtPosition(Vector2 position)
    {
        List<RaycastResult> results = new();
        _eventSystem.RaycastAll(new PointerEventData(_eventSystem) { position = position }, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject.TryGetComponent(out IClickHandler clickHandler))
            {
                clickHandler.OnClick();
                return;
            }
        }
    }

    public void Dispose() => 
            _uiController.ClickPerformed -= HandleClickAtPosition;
}