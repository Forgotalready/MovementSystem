using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Player")]
public class PlayerSettings : ScriptableObject
{
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public float JumpHeight { get; private set; }
}