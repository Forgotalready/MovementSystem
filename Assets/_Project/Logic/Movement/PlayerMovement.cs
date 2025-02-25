﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerSettings _playerSettings;

    private MovementController _movementController;
    private CharacterController _characterController;
    private Animator _animator;
    private DetectionComponent _detection;

    private readonly Dictionary<Type, IState> _playerStates = new();
    private IState _playerState;

    [Inject]
    private void Construct(MovementController movementController) =>
            _movementController = movementController;

    private void Start()
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
                new Moving(_characterController, _movementController, _animator, _playerSettings);
        _playerStates[typeof(Jump)] =
                new Jump(_characterController, _movementController, _animator, _playerSettings, _detection);
    }

    private void ChangeState(Type obj)
    {
        _playerState.OnExit();
        _playerState.StateChange -= ChangeState;
        _playerState = _playerStates[obj];
        _playerState.OnEnter();
        _playerState.StateChange += ChangeState;
    }


    private void Update()
    {
        Debug.Log(_playerState.GetType().Name);
        _playerState.OnUpdate(Time.deltaTime);
    }

    private void OnDestroy()
    {
        _playerState.OnExit();
        _playerState.StateChange -= ChangeState;
    }
}