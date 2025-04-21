using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellRepository
{
    private readonly List<GameObject> _activeCells = new();
    private readonly List<GameObject> _disableCells = new();

    private readonly int _maxDisableCells;

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="maxDisableCells">Максимальное количество хранимых скрытыми в ОП.</param>
    public CellRepository(int maxDisableCells = 10) =>
            _maxDisableCells = maxDisableCells;

    public void Add(GameObject cell) =>
            _activeCells.Add(cell);

    public bool TryGetDisable(out GameObject inactiveCell)
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

        if (_disableCells.Count() < _maxDisableCells)
        {
            _disableCells.Add(cell);
        }
        else
        {
            Object.Destroy(cell);
        }
    }
}