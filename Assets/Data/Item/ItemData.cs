using UnityEngine;

public enum ItemType
{
    Exp,
    Arrow
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public Sprite art;
    public ItemType itemType;
}
