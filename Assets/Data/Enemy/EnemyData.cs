using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int maxHealth;
    public float damage;
    public float speed;
    public List<ItemData> dropItems = new();
    public int expEarned;
    public int arrowEarned;
}
