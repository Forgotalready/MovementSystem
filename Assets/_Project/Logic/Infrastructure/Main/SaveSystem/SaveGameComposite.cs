using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SaveGameComposite : MonoBehaviour, ISaveable
{
    [InjectLocal]
    private List<ISaveable> _saveables = new();
    
    [Inject]
    private SaveService _saveService;

    public string SaveKey { get; private set; }

    private void Start()
    {
        SaveKey = $"Composite_{gameObject.name}";
        _saveService.Register(this);
    }

    public object SaveState()
    {
        return _saveables.ToDictionary(
                s => s.SaveKey,
                s => s.SaveState()
        );
    }

    public void RestoreState(object state)
    {
        if (state is Dictionary<string, object> stateDict)
        {
            foreach (var saveable in _saveables)
            {
                var saveableState = stateDict.GetValueOrDefault(saveable.SaveKey);
                saveable.RestoreState(saveableState);
            }
        }
    }
}