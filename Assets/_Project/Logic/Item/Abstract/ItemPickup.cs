using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [field: SerializeField] public Item ItemData { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerMovement>(out _))
        {
            Debug.Log($"{ItemData.Name} собран");
            Destroy(gameObject);
        }
    }
}