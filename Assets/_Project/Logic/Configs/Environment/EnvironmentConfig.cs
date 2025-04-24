using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Environment")]
public class EnvironmentConfig : ScriptableObject
{
    /// <summary>
    /// Ускорение свободного падения в среде
    /// </summary>
    [field: SerializeField] public float Gravity { get; private set; }
}