using System;

public interface ISaveable
{
    String SaveKey { get; }
    object SaveState();
    void RestoreState(object state);
}