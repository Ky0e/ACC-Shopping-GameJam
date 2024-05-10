using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Scriptable Objects/Loot Table/Loot Table", fileName = "New Loot Table", order = 0)]
public class LootTable : ScriptableObject
{
    [field: SerializeField, Label("Loot Table Name")] public string lootTableName;
    [field: SerializeField, Expandable] private List<LootItem> lootTable;

    public List<LootItem> GenerateRandomLoot(int _totalAmountOfItems)
    {
        if(lootTable == null || lootTable.Count <= 0) { Debug.LogError("Loot Table Is Empty"); return null; }

        lootTable.Sort((x, y) => x.SpawnChance.CompareTo(y.SpawnChance));

        float _randomNum = Random.Range(0f, 1f);
        List<LootItem> _itemsToReturn = new();

        for(int i = 0; i < _totalAmountOfItems; i++)
        {
            foreach (var _item in lootTable)
            {
                float _itemSpawnChance = _item.SpawnChance;
                if (_itemSpawnChance > _randomNum && _itemsToReturn.Count < _totalAmountOfItems)
                {
                    _itemsToReturn.Add(_item);
                }
            }
        }
         if (_itemsToReturn.Count <= 0) { GenerateRandomLoot(_totalAmountOfItems); }
        return _itemsToReturn;
    }
}
