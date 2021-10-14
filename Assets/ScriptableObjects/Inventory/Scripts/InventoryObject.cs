using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Malee.List;

[Serializable]
public abstract class InventoryObject<SlotType> : ScriptableObject where SlotType : InventorySlot
{
    private int maxSize;
    public float someFloat;
    public int MaxSize { get { return maxSize; } }
    public InventorySlots slots { get { return GetSlots(); } }

    [Serializable]
    public class InventorySlots : ReorderableArray<SlotType> { }

    protected InventoryObject(int _maxSize)
    {
        maxSize = _maxSize;
    }

    public abstract InventorySlots GetSlots();

    void OnEnable() {
        Debug.Log("Calling inventoryobject onenable");
        for (int i = 0; i < slots.Count; i++)
        {
            SlotType slot = slots[i];
            if (slot.item == null || slot.amount == 0)
            {
                Debug.Log("Removing item because null or amount 0");
                RemoveItem(slot);
                i--;
            }
        }
    }
    void OnValidate()
    {
        foreach (SlotType slot in slots)
        {
            if (slot.item != null)
            {
                if (slot.item.isSingleSlot || slot.amount <= 0)
                {
                    slot.amount = 1;
                }
            }
        }
        // Dont allow going over max size
        if (slots.Count > maxSize)
        {
            slots.RemoveAt(slots.Count - 1);
        }
    }

    public void AddItem(SlotType newSlot)
    {
        SlotType matchingSlot = FindSlot(newSlot);
        if (matchingSlot != null && !matchingSlot.item.isSingleSlot)
        {
            matchingSlot.amount += newSlot.amount;
        }
        else if (slots.Count < maxSize)
        {
            slots.Add(newSlot);
        }
        else
        {
            throw new NoSpaceInInventoryException();
        }
    }
    public SlotType FindSlot(SlotType slot)
    {
        foreach (SlotType it in slots)
        {
            if (it.item.name == slot?.item.name)
            {
                return it;
            }
        }
        return null;
    }
    public SlotType FindSlot(string itemName)
    {
        foreach (SlotType it in slots)
        {
            if (it.item.name == itemName)
            {
                return it;
            }
        }
        return null;
    }
    public void RemoveItem(SlotType slot)
    {
        Debug.Log("Removing item: " + slot?.item.name);
        slots.Remove(slot);
    }
    public void RemoveItem(string itemName)
    {
        slots.Remove(FindSlot(itemName));
    }

    public void SortSlots()
    {
        // slots.Sort((a, b) =>
        // {
        //     if ((a.item != null && b.item != null) || (a.item == null && b.item == null))
        //     {
        //         return 0;
        //     }
        //     if (a.item == null)
        //     {
        //         return 1;
        //     }
        //     else if (b.item == null)
        //     {
        //         return -1;
        //     }
        //     else return 0;
        // });
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
                SortSlots();
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


[Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount = 1;

}