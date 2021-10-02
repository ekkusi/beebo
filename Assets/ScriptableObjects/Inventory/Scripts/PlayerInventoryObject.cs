using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Inventory Object", menuName = "Inventory System/Player Inventory")]
public class PlayerInventoryObject : InventoryObject<PlayerInventorySlot>
{
    private const int MAX_INVENTORY_SIZE = 28;
    public PlayerInventoryObject() : base(MAX_INVENTORY_SIZE) { }
}

[Serializable]
public class PlayerInventorySlot : InventorySlot
{
    public PlayerInventorySlot(ItemObject _item, int _amount = 1)
    {
        item = _item;
        amount = item.isSingleSlot ? 1 : _amount;
    }
}