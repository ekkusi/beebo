using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Merchant Inventory Object", menuName = "Inventory System/Merchant Inventory")]
public class MerchantInventoryObject : InventoryObject<MerchantInventorySlot>
{
  private const int MAX_INVENTORY_SIZE = 20;
  public MerchantInventoryObject() : base(MAX_INVENTORY_SIZE)
  {
  }
}

[Serializable]
public class MerchantInventorySlot : InventorySlot
{

  public int price; 

  public MerchantInventorySlot(ItemObject _item, int _price, int _amount = 1) { 
      item = _item;
      amount = _amount;
      price = _price;
  }
}