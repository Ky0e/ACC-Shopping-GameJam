using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class Component_LootSpawner : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Loot Table Properties"), Expandable] private LootTable roomTable;
    [field: SerializeField, BoxGroup("Loot Table Properties")] private bool spawnRandomNumberOfObjects = false;
    [field: SerializeField, BoxGroup("Loot Table Properties"), HideIf("spawnRandomNumberOfObjects"), Label("Constant Number Of Items To Spawn")] private int constantNumberToSpawn;
    [field: SerializeField, BoxGroup("Loot Table Properties"), ShowIf("spawnRandomNumberOfObjects"), Label("Random Range Of Items To Spawn"), MinMaxSlider(0,20)] private Vector2Int randomNumberOfItemsToSpawn;
    [field: SerializeField, BoxGroup("Explosion Properties")] private float explosionForce;
    [field: SerializeField, BoxGroup("Explosion Properties")] private float explosionRange;

    [Button("Get Item Test", EButtonEnableMode.Always)]
    public void DebugItem()
    {
        Debug.LogWarning("List Start");
        List<LootItem> _itemList = roomTable.GenerateRandomLoot(5);
        foreach (LootItem item in _itemList)
        {
            Debug.Log(item.ItemName);
        }
        Debug.LogWarning("List End");
    }

    private void Update()
    {
    }


    public void OnLootSpawn()
    {
        int _numberOfItems = spawnRandomNumberOfObjects ? Random.Range(randomNumberOfItemsToSpawn.x, randomNumberOfItemsToSpawn.y) : constantNumberToSpawn;
        List <LootItem> _itemList = roomTable.GenerateRandomLoot(_numberOfItems);

        foreach (LootItem _item in _itemList) 
        {
            GameObject _tempOBJ = _item.SpawnLootItem();
            if(!_tempOBJ.TryGetComponent(out Rigidbody _component_Rigidbody)) { return; }
            _component_Rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRange);
        }
    }
    public void OnLootSpawn(int _totalAmountOfItems, Transform _parent)
    {
        int _numberOfItems = spawnRandomNumberOfObjects ? Random.Range(randomNumberOfItemsToSpawn.x, randomNumberOfItemsToSpawn.y) : constantNumberToSpawn;
        List<LootItem> _itemList = roomTable.GenerateRandomLoot(_numberOfItems);

        foreach (LootItem _item in _itemList) 
        { 
            GameObject _tempOBJ = _item.SpawnLootItem(_parent);
            if (!_tempOBJ.TryGetComponent(out Rigidbody _component_Rigidbody)) { return; }
            _component_Rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRange);
        }
    }
    public void OnLootSpawn(int _totalAmountOfItems, Vector3 _position, Quaternion _rotation, Transform _parent)
    {
        int _numberOfItems = spawnRandomNumberOfObjects ? Random.Range(randomNumberOfItemsToSpawn.x, randomNumberOfItemsToSpawn.y) : constantNumberToSpawn;
        List<LootItem> _itemList = roomTable.GenerateRandomLoot(_numberOfItems);

        foreach (LootItem _item in _itemList) 
        {
            GameObject _tempOBJ = _item.SpawnLootItem(_position, _rotation, _parent);
            if (!_tempOBJ.TryGetComponent(out Rigidbody _component_Rigidbody)) { return; }
            _component_Rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRange);
        }
    }
}
