using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[Serializable]
public abstract class InventoryObject<SlotType> : ScriptableObject where SlotType : InventorySlot
{
    private int maxSize;
    public int MaxSize { get { return maxSize; } }
    // [ReorderableList]
    [SerializeField]
    private List<SlotType> slots;

    protected InventoryObject(int _maxSize)
    {
        maxSize = _maxSize;
        slots = new List<SlotType>(new SlotType[maxSize]);
    }

    void OnValidate()
    {
        foreach (SlotType slot in slots)
        {
            if (slot?.item != null)
            {
                if (slot.item.isSingleSlot || slot.amount <= 0)
                {
                    slot.amount = 1;
                }
            }
        }
    }

    public void AddItem(SlotType newSlot)
    {
        List<SlotType> notNullSlots = GetItems();
        SlotType matchingSlot = notNullSlots.Find((slot) => slot.item.name == newSlot.item.name);
        if (matchingSlot != null && !matchingSlot.item.isSingleSlot)
        {
            matchingSlot.amount += newSlot.amount;
        }
        else if (notNullSlots.Count <= maxSize)
        {
            int firstNotNullIndex = slots.FindIndex((slot) => slot.item == null);
            Debug.Log("Adding item to index " + firstNotNullIndex);
            slots[firstNotNullIndex] = newSlot;
        }
        else
        {
            Debug.LogWarning("Inventory is already full with " + slots.Count + " items.");
        }
    }
    public void RemoveItem(string itemName)
    {
        List<SlotType> notNullSlots = GetItems();
        SlotType matchingSlot = notNullSlots.Find((slot) => slot.item.name == itemName);
        if (matchingSlot != null)
        {
            matchingSlot.item = null;
            slots.Sort((a, b) =>
            {
                if (a.item == null && b.item != null)
                {
                    return 1;
                }
                else if (a.item != null && b.item == null)
                {
                    return -1;
                }
                else return 0;
            });
        }
    }

    public void SubstractAmountOrRemove(string itemName, int amount = 1)
    {
        List<SlotType> notNullSlots = GetItems();
        SlotType matchingItem = notNullSlots.Find((slot) => slot.item.name == itemName);
        if (matchingItem != null)
        {
            matchingItem.amount -= amount;
            if (matchingItem.amount <= 0)
            {
                RemoveItem(itemName);
            }
        }
        else
        {
            Debug.LogError("No matching item found in inventory of item: " + itemName);
        }
    }


    public List<SlotType> GetItems()
    {
        List<SlotType> notNullSlots = new List<SlotType>();
        foreach (SlotType slot in slots)
        {
            if (slot.item != null)
            {
                notNullSlots.Add(slot);
            }
        }
        return notNullSlots;
    }
}


public class InventorySlot
{
    public ItemObject item;
    public int amount = 1;
}