using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Scriptable Objects/Loot Table/Loot Item", fileName = "New Loot Item", order = -1)]
public class LootItem : ScriptableObject
{
    [field: SerializeField, BoxGroup("Item Properties"), Label("Name")] public string ItemName { get; private set; }
    [field: SerializeField, BoxGroup("Item Properties")] public int ID { get; private set; }
    [field: SerializeField, BoxGroup("Item Properties")] private string description;
    [field: SerializeField, BoxGroup("Item Properties"), Label("Loot Prefab")] private GameObject lootItemPrefab;

    [field: SerializeField, Range(0.01f, 1.0f)] public float SpawnChance;


    public GameObject SpawnLootItem() => Instantiate(lootItemPrefab);

    public GameObject SpawnLootItem(Transform _parent) => Instantiate(lootItemPrefab, _parent);

    public GameObject SpawnLootItem(Vector3 _position, Quaternion _rotation, Transform _parent) => Instantiate(lootItemPrefab, _position, _rotation, _parent);
}