using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private GameObject _cellContainer;
    [SerializeField] private Image _cellPrefab;

    private Inventory _inventory;
    
    [Inject]
    private void Construct(Inventory inventory) => _inventory = inventory;

    private void Start() => _inventory.InventoryChange += OnInventoryChange;

    private void OnInventoryChange(IEnumerable inventory)
    {
        ClearContainer();
        foreach (Item item in inventory)
        {
            Image createdImage = Instantiate(_cellPrefab, _cellContainer.transform);
            createdImage.sprite = item.Icon;
        }
    }

    private void ClearContainer()
    {
        foreach (Transform child in _cellContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnDestroy() => _inventory.InventoryChange -= OnInventoryChange;
}