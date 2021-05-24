using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

// [Serializable]
// public class InventorySlotDict : SerializableDictionary<string, IInventorySlot>{}
[Serializable]
public abstract class InventoryObject<SlotType> : ScriptableObject where SlotType : IInventorySlot 
{
    [ReorderableList]
    [SerializeField]
    List<SlotType> inventory = new List<SlotType>();
    public void AddItem(SlotType newSlot) {
        SlotType matchingItem = inventory.Find((slot) => slot.item.name == newSlot.item.name);
        if (matchingItem != null) {
            matchingItem.amount += newSlot.amount;
        } else {
            inventory.Add(newSlot);
        }
    }
    
    public void SubstractItemAmount(string itemName, int amount = 1) {
        SlotType matchingItem = inventory.Find((slot) => slot.item.name == itemName);
        if (matchingItem != null) {
            matchingItem.amount -= amount;
            if (matchingItem.amount <= 0) {
                inventory.Remove(matchingItem);
            }
        } else {
            Debug.LogError("No matching item found in inventory of item: " + itemName);
        }
    }
}

[Serializable]
public class IInventorySlot {
    [SerializeField]
    public ItemObject item; 
    [SerializeField]
    public int amount; 

}