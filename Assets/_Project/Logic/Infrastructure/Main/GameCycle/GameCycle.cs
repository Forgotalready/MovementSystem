using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс представляет собой реализацию игрового цикла.
/// </summary>
public class GameCycle : MonoBehaviour
{
    private List<IGameListener> _gameListeners = new();

    private List<IGameUpdateListener> _updateListeners = new();
    private List<IGameFixedUpdateListener> _fixedUpdateListeners = new();
    private List<IGameLateUpdateListener> _lateUpdateListeners = new();

    public GameState State { get; private set; } = GameState.Off;
    
    public void AddListener(IGameListener listener)
    {
        _gameListeners.Add(listener);
        CategorizeListener(listener);
    }

    private void CategorizeListener(IGameListener listener)
    {
        if (listener is IGameUpdateListener updateListener)
        {
            _updateListeners.Add(updateListener);
        }
            
        if (listener is IGameFixedUpdateListener fixedUpdateListener)
        {
            _fixedUpdateListeners.Add(fixedUpdateListener);
        }

        if (listener is IGameLateUpdateListener lateUpdateListener)
        {
            _lateUpdateListeners.Add(lateUpdateListener);
        }
    }

    private void Start() => 
            GameStart();

    public void GameStart()
    {
        if (State != GameState.Off)
        {
            return;
        }

        State = GameState.Playing;

        foreach (IGameListener listener in _gameListeners)
        {
            if (listener is IGameStartListener startListener)
            {
                startListener.OnGameStart();
            }
        }
    }

    private void Update()
    {
        if (State != GameState.Playing)
        {
            return;
        }
        
        foreach (var updateListener in _updateListeners)
        {
            float deltaTime = Time.deltaTime;
            updateListener.OnUpdate(deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (State != GameState.Playing)
        {
            return;
        }
        foreach (var fixedUpdateListener in _fixedUpdateListeners)
        {
            float deltaTime = Time.fixedDeltaTime;
            fixedUpdateListener.OnFixedUpdate(deltaTime);
        }
    }

    private void LateUpdate()
    {
        if (State != GameState.Playing)
        {
            return;
        }
        
        foreach (var lateUpdateListener in _lateUpdateListeners)
        {
            float deltaTime = Time.deltaTime;
            lateUpdateListener.OnLateUpdate(deltaTime);
        }
    }

    public void GamePaused()
    {
        if (State != GameState.Playing)
        {
            return;
        }

        State = GameState.Pause;
        
        foreach (var listener in _gameListeners)
        {
            if (listener is IGamePauseListener pauseListener)
            {
                pauseListener.OnGamePause();
            }
        }
    }

    public void GameResume()
    {
        if (State != GameState.Pause)
        {
            return;
        }

        State = GameState.Playing;
        
        foreach (var listener in _gameListeners)
        {
            if (listener is IGameResumeListener resumeListener)
            {
                resumeListener.OnGameResume();
            }
        }
    }

    public void GameFinish()
    {
        if (State == GameState.Off)
        {
            return;
        }

        State = GameState.Off;
        
        foreach (IGameListener listener in _gameListeners)
        {
            if (listener is IGameFinishListener finishListener)
            {
                finishListener.OnGameFinish();
            }
        }
    }

    private void OnDestroy() => 
            GameFinish();
}
