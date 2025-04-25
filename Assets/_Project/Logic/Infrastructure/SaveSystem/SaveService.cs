using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class SaveService
{
    private readonly Dictionary<String, ISaveable> _saveables = new();
    private readonly IStrorage _storage;

    private readonly string _defaultSlot = "default";

    public SaveService(IStrorage storage) =>
            _storage = storage;

    public void Register(ISaveable s) =>
            _saveables[s.SaveKey] = s;

    public void Unregister(ISaveable s) =>
            _saveables.Remove(s.SaveKey);

    public void Save(String slot = null)
    {
        var data = new SaveData
        {
                State = _saveables.Values.ToDictionary(
                        s => s.SaveKey,
                        s => s.SaveState()
                )
        };
        var json = JsonConvert.SerializeObject(
                data,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }
        );

        _storage.Save(json, slot ?? _defaultSlot);
    }

    public void Load(String slot = null)
    {
        slot ??= _defaultSlot;
        if (!_storage.Exists(slot))
        {
            Debug.LogWarning($"No save data found for slot {slot}");
        }

        var json = _storage.Load(slot);
        var data = JsonConvert.DeserializeObject<SaveData>(
                json,
                new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All }
        );

        foreach (var keyValue in data.State)
        {
            if (_saveables.TryGetValue(keyValue.Key, out ISaveable saveable))
            {
                saveable.RestoreState(keyValue.Value);
            }
        }
    }

    public void Delete(String slot = null)
    {
        slot ??= _defaultSlot;
        _storage.Delete(slot);
    }
}