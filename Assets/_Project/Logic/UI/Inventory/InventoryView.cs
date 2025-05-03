using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryView : MonoBehaviour, IGameStartListener, IGameFinishListener
{
    [SerializeField] private GameObject _cellContainer;
    [SerializeField] private GameObject _cellPrefab;

    private CellFactory _cellFactory;
    private Inventory _inventory;
    private UIController _uiController;

    [Inject]
    private void Construct(Inventory inventory, UIController uiController)
    {
        _inventory = inventory;
        _uiController = uiController;
    }

    public void OnGameStart()
    {
        _cellFactory = new CellFactory(_cellPrefab);
        _inventory.InventoryChange += OnInventoryChange;
        _uiController.InventoryOpened += OnInventoryOpened;
        gameObject.SetActive(false); // TODO() Придумать как это переделать
    }

    private void OnInventoryOpened() =>
            gameObject.SetActive(!gameObject.activeSelf);

    private void OnInventoryChange(ReadOnlyCollection<Item> inventory)
    {
        ClearContainer();
        foreach (Item item in inventory)
        {
            GameObject createdImage = _cellFactory.CreateCell(_cellContainer.transform);
            createdImage.GetComponent<CellClickHandler>().CellClicked += OnCellClicked;
            createdImage.GetComponent<Image>().sprite = item.Icon;
        }
    }

    private void OnCellClicked(int index) =>
            _inventory.Use(index);

    private void ClearContainer()
    {
        foreach (Transform child in _cellContainer.transform)
        {
            child.GetComponent<CellClickHandler>().CellClicked -= OnCellClicked;
            _cellFactory.DeleteCell(child.gameObject);
        }
    }

    public void OnGameFinish() =>
            _inventory.InventoryChange -= OnInventoryChange;
}