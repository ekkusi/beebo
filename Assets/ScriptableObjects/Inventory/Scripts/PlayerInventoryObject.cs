using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Inventory Object", menuName = "Inventory System/Player Inventory")]
public class  PlayerInventoryObject : InventoryObject<PlayerInventorySlot> 
{
}

[Serializable]
public class PlayerInventorySlot : IInventorySlot {
    public PlayerInventorySlot(ItemObject _item, int _amount = 1) {
        item = _item;
        amount = _amount;
    }
}