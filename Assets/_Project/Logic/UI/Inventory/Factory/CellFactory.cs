using UnityEngine;

/// <summary>
/// Класс предназначенный для создания ячеек инвентаря.
/// !!!! Не нужно пытаться обмануть систему и пытаться создать/удалить ячейку не через этот класс.
/// Это приведёт к невероятной магии, вы такого даже в хогвартсе не видели !!!!
/// </summary>
public class CellFactory
{
    private readonly GameObject _cellPrefab;
    private readonly CellRepository _cellRepository = new();

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="cellPrefab">Шаблон ячейки</param>
    public CellFactory(GameObject cellPrefab)
    {
        _cellPrefab = cellPrefab;
    }

    /// <summary>
    /// Метод "создания" клетки
    /// </summary>
    /// <param name="parent">Компонент трансформ контейнера для ячеек</param>
    /// <returns>Ячейка</returns>
    public GameObject CreateCell(Transform parent)
    {
        if (_cellRepository.TryGetDisable(out GameObject cell))
        {
            cell.SetActive(true);
            return cell;
        }

        cell = Object.Instantiate(_cellPrefab, parent);
        _cellRepository.Add(cell);
        return cell;
    }

    public void DeleteCell(GameObject cell)
    {
        cell.SetActive(false);
        _cellRepository.Delete(cell);
    }
}