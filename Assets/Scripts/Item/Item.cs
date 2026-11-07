using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData data;

    public ItemType ItemType => data.itemType;
}
