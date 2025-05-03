using System;
using UnityEngine;

public class PlayerSaveComponent : MonoBehaviour, ISaveable
{
    private SaveService _saveService;

    public String SaveKey =>
            "Player";
    
    public object SaveState()
    {
        return new PlayerState
        {
                Position = transform.position.ToAdapter(),
                Rotation = transform.rotation.ToAdapter()
        };
    }

    public void RestoreState(object state)
    {
        if (state is PlayerState playerState)
        {
            CharacterController characterController = GetComponent<CharacterController>();
            characterController.enabled = false;
            transform.position = playerState.Position.ToUnity();
            transform.rotation = playerState.Rotation.ToUnity();
            characterController.enabled = true;
        }
    }

    [Serializable]
    private struct PlayerState
    {
        public Vector3Adapter Position;
        public QuaternionAdapter Rotation;
    }
}