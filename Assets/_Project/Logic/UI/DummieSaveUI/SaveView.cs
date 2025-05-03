using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveView : MonoBehaviour, IGameStartListener, IGameFinishListener
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _deleteButton;

    public event Action SaveButtonClicked;
    public event Action LoadButtonClicked;
    public event Action DeleteButtonClicked;
    
    public void OnGameStart()
    {
        _saveButton.onClick.AddListener(OnSaveButtonClicked);
        _loadButton.onClick.AddListener(OnLoadButtonClicked);
        _deleteButton.onClick.AddListener(OnDeleteButtonClicked);
    }

    private void OnDeleteButtonClicked() => 
            DeleteButtonClicked?.Invoke();

    private void OnLoadButtonClicked() => 
            LoadButtonClicked?.Invoke();

    private void OnSaveButtonClicked() => 
            SaveButtonClicked?.Invoke();

    public void OnGameFinish()
    {
        _saveButton.onClick.RemoveListener(OnSaveButtonClicked);
        _loadButton.onClick.RemoveListener(OnLoadButtonClicked);
        _deleteButton.onClick.RemoveListener(OnDeleteButtonClicked);
    }
}