using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
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
            Debug.Log(transform.position);
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