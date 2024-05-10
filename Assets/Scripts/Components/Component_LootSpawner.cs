using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

public class Component_LootSpawner : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Loot Table Properties"), Expandable] private LootTable roomTable;
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


    public void OnLootSpawn(int _totalAmountOfItems)
    {
        List<LootItem> _itemList = roomTable.GenerateRandomLoot(_totalAmountOfItems);

        foreach (LootItem _item in _itemList) 
        {
            GameObject _tempOBJ = _item.SpawnLootItem();
            if(!_tempOBJ.TryGetComponent(out Rigidbody _component_Rigidbody)) { return; }
            _component_Rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRange);
        }
    }
    public void OnLootSpawn(int _totalAmountOfItems, Transform _parent)
    {
        List<LootItem> _itemList = roomTable.GenerateRandomLoot(_totalAmountOfItems);

        foreach (LootItem _item in _itemList) 
        { 
            GameObject _tempOBJ = _item.SpawnLootItem(_parent);
            if (!_tempOBJ.TryGetComponent(out Rigidbody _component_Rigidbody)) { return; }
            _component_Rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRange);
        }
    }
    public void OnLootSpawn(int _totalAmountOfItems, Vector3 _position, Quaternion _rotation, Transform _parent)
    {
        List<LootItem> _itemList = roomTable.GenerateRandomLoot(_totalAmountOfItems);

        foreach (LootItem _item in _itemList) 
        {
            GameObject _tempOBJ = _item.SpawnLootItem(_position, _rotation, _parent);
            if (!_tempOBJ.TryGetComponent(out Rigidbody _component_Rigidbody)) { return; }
            _component_Rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRange);
        }
    }
}
