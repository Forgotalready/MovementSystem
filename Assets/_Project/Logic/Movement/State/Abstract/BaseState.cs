﻿using System;
using UnityEngine;

public abstract class BaseState : IState
{
    protected CharacterController CharacterController;
    protected MovementController MovementController;
    protected Animator Animator;
    protected PlayerSettings PlayerSettings;
    protected EnvironmentConfig EnvironmentConfig;
    private Transform _playerTransform;

    protected BaseState(CharacterController characterController, MovementController movementController,
            Animator animator, PlayerSettings playerSettings, EnvironmentConfig environmentConfig)
    {
        CharacterController = characterController;
        MovementController = movementController;
        Animator = animator;
        PlayerSettings = playerSettings;
        EnvironmentConfig = environmentConfig;
        _playerTransform = CharacterController.gameObject.transform;
    }

    protected Vector3 ToLocalCoordinates(Vector3 globalDirection) =>
            _playerTransform.TransformDirection(globalDirection);

    public abstract event Action<Type> StateChange;

    public abstract void OnEnter();

    public abstract void HandleInput();

    public abstract void OnUpdate(float deltaTime);

    public abstract void OnExit();
    public void SetEnvironmentConfig(EnvironmentConfig environmentConfig)
    {
        EnvironmentConfig = environmentConfig;
    }
}