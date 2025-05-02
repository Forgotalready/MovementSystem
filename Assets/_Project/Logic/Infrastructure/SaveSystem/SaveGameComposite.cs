using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SaveGameComposite : MonoBehaviour, ISaveable
{
    private Dictionary<String, ISaveable> _saveables = new();
    private SaveService _saveService;

    public string SaveKey =>
            "Composite";

    [Inject]
    private void Construct(ISaveable[] saveables, SaveService saveService)
    {
        _saveables = saveables.ToDictionary(s => s.SaveKey);
        _saveService = saveService;
    }

    private void Start() =>
            _saveService.Register(this);

    public object SaveState()
    {
        return _saveables.Values.ToDictionary(
                s => s.SaveKey,
                s => s.SaveState()
        );
    }

    public void RestoreState(object state)
    {
        if (state is Dictionary<string, object> stateDict)
        {
            foreach (var saveable in _saveables.Values)
            {
                var saveableState = stateDict.GetValueOrDefault(saveable.SaveKey);
                saveable.RestoreState(saveableState);
            }
        }
    }
}