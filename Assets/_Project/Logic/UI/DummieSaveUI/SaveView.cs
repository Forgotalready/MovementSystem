using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveView : MonoBehaviour
{
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _deleteButton;

    public event Action SaveButtonClicked;
    public event Action LoadButtonClicked;
    public event Action DeleteButtonClicked;
    
    private void Start()
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
}