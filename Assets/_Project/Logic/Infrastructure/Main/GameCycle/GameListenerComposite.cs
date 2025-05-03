using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameListenerComposite : MonoBehaviour,
        IGameStartListener,
        IGameUpdateListener,
        IGameFixedUpdateListener,
        IGameLateUpdateListener,
        IGamePauseListener,
        IGameResumeListener,
        IGameFinishListener
{
    [Inject] private GameCycle _gameCycle;
    
    [InjectLocal] List<IGameListener> _gameListeners = new();
    private List<IGameUpdateListener> _updateListeners = new();
    private List<IGameFixedUpdateListener> _fixedUpdateListeners = new();
    private List<IGameLateUpdateListener> _lateUpdateListeners = new();

    private void Start()
    {
        _gameCycle.AddListener(this);
        foreach (IGameListener listener in _gameListeners)
        {
            CategorizeListener(listener);
        }
    }

    public void OnGameStart()
    {
        foreach (IGameListener listener in _gameListeners)
        {
            if (listener is IGameStartListener startListener)
            {
                startListener.OnGameStart();
            }
        }
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var updateListener in _updateListeners)
        {
            updateListener.OnUpdate(deltaTime);
        }
    }

    public void OnFixedUpdate(float deltaTime)
    {
        foreach (var fixedUpdateListener in _fixedUpdateListeners)
        {
            fixedUpdateListener.OnFixedUpdate(deltaTime);
        }
    }

    public void OnLateUpdate(float deltaTime)
    {
        foreach (var lateUpdateListener in _lateUpdateListeners)
        {
            lateUpdateListener.OnLateUpdate(deltaTime);
        }
    }
    
    public void OnGamePause()
    {
        foreach (var listener in _gameListeners)
        {
            if (listener is IGamePauseListener pauseListener)
            {
                pauseListener.OnGamePause();
            }
        }
    }

    public void OnGameResume()
    {
        foreach (var listener in _gameListeners)
        {
            if (listener is IGameResumeListener resumeListener)
            {
                resumeListener.OnGameResume();
            }
        }
    }

    public void OnGameFinish()
    {
        foreach (IGameListener listener in _gameListeners)
        {
            if (listener is IGameFinishListener finishListener)
            {
                finishListener.OnGameFinish();
            }
        }
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

}