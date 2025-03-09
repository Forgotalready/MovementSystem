using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellRepository
{
    private readonly List<GameObject> _activeCells = new();
    private readonly List<GameObject> _disableCells = new();

    public void Add(GameObject cell) => _activeCells.Add(cell);

    public bool TryGetInactive(out GameObject inactiveCell)
    {
        if (_disableCells.Count() != 0)
        {
            inactiveCell = _disableCells[0];
            _disableCells.Remove(inactiveCell);
            _activeCells.Add(inactiveCell);
            return true;
        }
        inactiveCell = null;
        return false;
    }

    public void Delete(GameObject cell)
    {
        if (!_activeCells.Remove(cell))
        {
            Debug.LogWarning($"CellRepository: {cell.name} не объект созданный фабрикой");
            return;
        }

        cell.SetActive(false);
        _disableCells.Add(cell);
    }
}