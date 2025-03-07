using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private GameObject _cellContainer;
    [SerializeField] private GameObject _cellPrefab;

    private CellFactory _cellFactory;
    private Inventory _inventory;
    
    [Inject]
    private void Construct(Inventory inventory) => _inventory = inventory;

    private void Start()
    {
        _cellFactory = new CellFactory( _cellPrefab);
        _inventory.InventoryChange += OnInventoryChange;
    }

    private void OnInventoryChange(IEnumerable inventory)
    {
        ClearContainer();
        foreach (Item item in inventory)
        {
            Image createdImage = _cellFactory.CreateCell(_cellContainer.transform).GetComponent<Image>();
            createdImage.sprite = item.Icon;
        }
    }

    private void ClearContainer()
    {
        foreach (Transform child in _cellContainer.transform)
        {
            _cellFactory.DeleteCell(child.gameObject);
        }
    }

    private void OnDestroy() => _inventory.InventoryChange -= OnInventoryChange;
}