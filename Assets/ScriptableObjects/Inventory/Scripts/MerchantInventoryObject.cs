using System;
using System.Collections;
using System.Collections.Generic;
using Malee.List;
using UnityEngine;


[CreateAssetMenu(fileName = "New Merchant Inventory Object", menuName = "Inventory System/Merchant Inventory")]
[Serializable]
public class MerchantInventoryObject : InventoryObject<MerchantInventorySlot>
{
    public float buyPriceMultiplier = 1.0f;
    public float sellPriceMultiplier = 0.5f;
    private const int MAX_INVENTORY_SIZE = 28;
    [SerializeField, Reorderable]
    public MerchantInventorySlots merchantSlots;

    [Serializable]
    public class MerchantInventorySlots : InventorySlots { }
    public MerchantInventoryObject() : base(MAX_INVENTORY_SIZE) { }

    public override InventorySlots GetSlots()
    {
        return merchantSlots;
    }
}

[Serializable]
public class MerchantInventorySlot : InventorySlot
{
    public MerchantInventorySlot(ItemObject _item, int _amount = 1)
    {
        item = _item;
        amount = item.isSingleSlot ? 1 : _amount;
    }
}