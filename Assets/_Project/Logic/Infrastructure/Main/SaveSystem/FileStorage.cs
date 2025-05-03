using System;
using System.IO;
using UnityEngine;

public class FileStorage : IStrorage
{
    private readonly String _path;

    public FileStorage(String subFolder)
    {
        _path = Path.Combine(Application.persistentDataPath, subFolder);

        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
    }

    private String GetPath(String slotName) =>
            Path.Combine(_path, $"{slotName}.json");

    public void Save(String json, String slotName)
    {
        try
        {
            String path = GetPath(slotName);
            File.WriteAllText(path, json);
#if UNITY_EDITOR
            Debug.Log($"[FileStorage] Save to {path}");
#endif
        }
        catch (Exception e)
        {
            Debug.LogError($"[FileStorage] Save error: {e.Message}");
        }
    }

    public String Load(String slotName)
    {
        try
        {
            String path = GetPath(slotName);
            if (File.Exists(path))
            {
                String json = File.ReadAllText(path);
#if UNITY_EDITOR
                Debug.Log($"[FileStorage] Load from {path}");
#endif
                return json;
            }
            else
            {
                throw new FileNotFoundException($"Save file not found at {path}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[FileStorage] Load error: {e.Message}");
            return null;
        }
    }

    public bool Exists(String slotName)
    {
        string path = GetPath(slotName);
        return File.Exists(path);
    }

    public bool Delete(string slotName)
    {
        try
        {
            string path = GetPath(slotName);
            if (File.Exists(path))
            {
                File.Delete(path);
#if UNITY_EDITOR
                Debug.Log($"[FileStorage] Delete {path}");
#endif
                return true;
            }
            else
            {
                throw new FileNotFoundException($"Save file not found at {path}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[FileStorage] Delete error: {e.Message}");
            return false;
        }
    }
}