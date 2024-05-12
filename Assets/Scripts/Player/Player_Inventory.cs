using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    [SerializeField] private List<CardSO> items = new List<CardSO>();
    public static Player_Inventory Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool HasItem(CardSO _item)
    {
        // check if the player has the item
        //Debug.Log("Checking if player has item: " + _item.name + " - " + items.Contains(_item));
        return items.Contains(_item);
    }

    public void AddItem(CardSO _item)
    {
        items.Add(_item);
        // trigger modifier reset/update
    }

    public void RemoveItem(CardSO _item)
    {
        if(items.Contains(_item))
        {
            items.Remove(_item);
        }
        else
        {
            Debug.Log("Item '" + _item.name + "' not found in inventory!");
        }

        // trigger modifier reset/update

    }
}
