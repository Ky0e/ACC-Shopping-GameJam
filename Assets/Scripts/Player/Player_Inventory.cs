using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    [SerializeField] private List<CardSO> items = new List<CardSO>();
    public static Player_Inventory Instance;
    public Action OnItemsListUpdated;
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

    public bool HasCardOfType(eCardType _cardType)
    {
        CardSO _card = items.Find(x => x.cardType == _cardType);
        return _card != null;
    }

    public void AddItem(CardSO _item)
    {
        items.Add(_item);
        OnItemsListUpdated?.Invoke();
        // trigger modifier reset/update
    }

    public void RemoveItem(CardSO _item)
    {
        if(items.Contains(_item))
        {
            items.Remove(_item);
            OnItemsListUpdated?.Invoke();
        }
        else
        {
            Debug.Log("Item '" + _item.name + "' not found in inventory!");
        }

        // trigger modifier reset/update

    }
}
