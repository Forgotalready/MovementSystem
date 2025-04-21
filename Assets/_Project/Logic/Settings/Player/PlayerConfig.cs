using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Player")]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float MaxSpeed { get; private set; }
    
    [field: SerializeField] public float JumpHeight { get; private set; }
    
    /// <summary>
    /// Вертикальная скорость по умолчанию, для случая, если ускорение свободного падения равно 0.
    /// TODO() Лучше как-нибудь вычислять, а не строго задавать.
    /// </summary>
    [field: SerializeField] public float DefaultVerticalVelocity { get; private set; }
}