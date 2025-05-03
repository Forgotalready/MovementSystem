using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerMovement : MonoBehaviour, IGameStartListener, IGameUpdateListener, IGameFinishListener
{
    [SerializeField] private EnvironmentConfig _environmentConfig;

    private MovementController _movementController;
    private PlayerConfig _playerConfig;
    
    private CharacterController _characterController;
    private Animator _animator;
    private DetectionComponent _detection;

    private readonly Dictionary<Type, IState> _playerStates = new();
    private IState _playerState;

    [Inject]
    private void Construct(MovementController movementController, PlayerConfig playerConfig)
    {
        _movementController = movementController;
        _playerConfig = playerConfig;
    }

    public void OnGameStart()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _detection = GetComponent<DetectionComponent>();
        CreateStates();

        _playerState = _playerStates[typeof(Moving)];
        _playerState.StateChange += ChangeState;
        _playerState.OnEnter();
    }
    
    private void CreateStates()
    {
        _playerStates[typeof(Moving)] =
                new Moving(_characterController, _movementController, _animator, _playerConfig, _environmentConfig,
                        _detection);
        _playerStates[typeof(Jump)] =
                new Jump(_characterController, _movementController, _animator, _playerConfig, _environmentConfig,
                        _detection);
        _playerStates[typeof(Falling)] =
                new Falling(_characterController, _movementController, _animator, _playerConfig, _environmentConfig,
                        _detection);
    }

    private void ChangeState(Type obj)
    {
        if (!_playerStates.ContainsKey(obj))
        {
            return;
        }

        _playerState.OnExit();
        _playerState.StateChange -= ChangeState;
        _playerState = _playerStates[obj];
        _playerState.OnEnter();
        _playerState.StateChange += ChangeState;
    }
    
    public void OnUpdate(float deltaTime)
    {
        Debug.Log(_playerState.GetType().Name);
        _playerState.OnUpdate(deltaTime);
    }
    
    public void SetEnvironmentConfig(EnvironmentConfig environmentConfig)
    {
        _environmentConfig = environmentConfig;
        _playerStates.Values.ToList().ForEach(state => state.SetEnvironmentConfig(environmentConfig));
    }
    
    public void OnGameFinish()
    {
        _playerState.OnExit();
        _playerState.StateChange -= ChangeState;
    }
}