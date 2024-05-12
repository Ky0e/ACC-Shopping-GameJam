using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    [SerializeField] private List<CardSO> items = new List<CardSO>();
    public static Player_Inventory Instance;
    public Action OnItemsListUpdated;
    public SoHub soHub;

    private Player player;
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

        player = gameObject.GetComponent<Player>();
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
        // trigger modifier reset
        OnItemsListUpdated?.Invoke();
        //apply modifier
        player.ModifyProperty(_item.modifiedPropertyA, _item.modifierValueA);
        player.ModifyProperty(_item.modifiedPropertyB, _item.modifierValueB);
        player.ModifyProperty(_item.modifiedPropertyC, _item.modifierValueC);
        player.ModifyProperty(_item.modifiedPropertyD, _item.modifierValueD);
    }

    public void RemoveItem(CardSO _item)
    {
        if(items.Contains(_item))
        {
            items.Remove(_item);
            // trigger modifier reset
            OnItemsListUpdated?.Invoke();
            // remove modifier
            player.ModifyProperty(_item.modifiedPropertyA, -_item.modifierValueA);
            player.ModifyProperty(_item.modifiedPropertyB, -_item.modifierValueB);
            player.ModifyProperty(_item.modifiedPropertyC, -_item.modifierValueC);
            player.ModifyProperty(_item.modifiedPropertyD, -_item.modifierValueD);
        }
        else
        {
            Debug.Log("Item '" + _item.name + "' not found in inventory!");
        }
    }
}
