using System;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor;

[Serializable]
public abstract class InventoryObject<SlotType> : ScriptableObject where SlotType : InventorySlot 
{
    private int maxSize; 
    public int MaxSize { get { return maxSize; } }
    // [ReorderableList]
    [SerializeField]
    private List<SlotType> slots; 

    protected InventoryObject(int _maxSize) {
        maxSize = _maxSize;
        slots = new List<SlotType>(new SlotType[maxSize]);
    }

    public void AddItem(SlotType newSlot) {
        SlotType matchingSlot = slots.Find((slot) => slot.item.name == newSlot.item.name);
        if (matchingSlot != null && !matchingSlot.item.isSingleSlot) {
            matchingSlot.amount += newSlot.amount;
        } else if (slots.Count <= maxSize) {
            slots.Add(newSlot);
        } else {
            Debug.LogWarning("Inventory is already full with " + slots.Count + " items.");
        }
    }
    public void RemoveItem(string itemName) {
        slots.Remove(slots.Find((slot) => slot.item.name == itemName));
    }
    
    public void SubstractAmountOrRemove(string itemName, int amount = 1) {
        SlotType matchingItem = slots.Find((slot) => slot.item.name == itemName);
        if (matchingItem != null) {
            matchingItem.amount -= amount;
            if (matchingItem.amount <= 0) {
                slots.Remove(matchingItem);
            }
        } else {
            Debug.LogError("No matching item found in inventory of item: " + itemName);
        }
    }


    public List<SlotType> GetItems() {
        List<SlotType> notNullSlots = new List<SlotType>();
        foreach (SlotType slot in slots) {
            if (slot.item != null) {
                notNullSlots.Add(slot);
            }
        }
        return notNullSlots;
    }
}


public class InventorySlot {
    public ItemObject item; 
    [DrawIf("item.isSingleSlot", false, ComparisonType.Equals)]
    public int amount; 
}