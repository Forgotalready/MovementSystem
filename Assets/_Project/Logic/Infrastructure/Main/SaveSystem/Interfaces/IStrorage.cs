using System;

public interface IStrorage
{
    void Save(String json, String slotName);
    String Load(String slotName);
    bool Exists(String slotName);
    bool Delete(String slotName);
}