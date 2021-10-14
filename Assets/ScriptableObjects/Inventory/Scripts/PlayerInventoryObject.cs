using System;
using System.Collections;
using System.Collections.Generic;
using Malee.List;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Inventory Object", menuName = "Inventory System/Player Inventory")]
[Serializable]
public class PlayerInventoryObject : InventoryObject<PlayerInventorySlot>
{
    private const int MAX_INVENTORY_SIZE = 28;
    [SerializeField, Reorderable]
    public PlayerInventorySlots playerSlots;

    [Serializable]
    public class PlayerInventorySlots : InventorySlots { }
    public PlayerInventoryObject() : base(MAX_INVENTORY_SIZE) { }

    public override InventorySlots GetSlots()
    {
        return playerSlots;
    }
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