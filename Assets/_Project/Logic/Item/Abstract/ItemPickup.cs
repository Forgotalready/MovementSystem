using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item _itemData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerMovement>(out _))
        {
            Debug.Log($"{_itemData.Name} собран");
            Destroy(gameObject);
        }
    }
}