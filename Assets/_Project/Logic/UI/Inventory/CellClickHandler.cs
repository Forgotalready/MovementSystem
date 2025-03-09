using System;
using UnityEngine;

public class CellClickHandler : MonoBehaviour, IClickHandler
{
    public event Action<int> CellClicked;
    
    public void OnClick() => CellClicked?.Invoke(transform.GetSiblingIndex());
}