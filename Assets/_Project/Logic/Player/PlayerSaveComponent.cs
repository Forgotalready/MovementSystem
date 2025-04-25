using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerSaveComponent : MonoBehaviour, ISaveable
{
    private SaveService _saveService;

    public String SaveKey
    {
        get => "Player";
    }

    [Inject]
    private void Construct(SaveService saveService) =>
            _saveService = saveService;

    private void Start() =>
            _saveService.Register(this);

    private void OnDestroy() =>
            _saveService.Unregister(this);

    public object SaveState()
    {
        return new PlayerState
        {
                Position = new List<float> { transform.position.x, transform.position.y, transform.position.z },
                Rotation = new List<float>
                {
                        transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                        transform.rotation.eulerAngles.z
                }
        };
    }

    public void RestoreState(object state)
    {
        if (state is PlayerState playerState)
        {
            CharacterController characterController = GetComponent<CharacterController>();
            characterController.enabled = false;
            transform.position = new Vector3(playerState.Position[0], playerState.Position[1], playerState.Position[2]);
            transform.rotation =
                    Quaternion.Euler(playerState.Rotation[0], playerState.Rotation[1], playerState.Rotation[2]);
            characterController.enabled = true;
        }
    }

    [Serializable]
    private struct PlayerState
    {
        public List<float> Position;
        public List<float> Rotation;
    }
}