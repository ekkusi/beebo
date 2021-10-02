using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    public RectTransform equipmentPanel;
    private PlayerInventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = gameObject.transform.parent.GetComponentInChildren<PlayerInventoryManager>();
        if (inventoryManager == null)
        {
            Debug.LogError("No inventory manager found in sibling. Add inventory manager to a sibling of this object for equipment to work properly.");
        }
        equipmentPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            equipmentPanel.gameObject.SetActive(!equipmentPanel.gameObject.activeInHierarchy);
        }
    }

    public void EquipItem(EquipmentObject item)
    {
        EquipmentSlot slot = item.GetSlot();
        Dictionary<EquipmentSlot, EquipmentObject> equippedItems = GetEquippedItems();
        if (equippedItems.ContainsKey(slot))
        {
            inventoryManager.RemoveItem(item.name);
            inventoryManager.inventory.AddItem(new PlayerInventorySlot(item));
        }
        else
        {
            inventoryManager.RemoveItem(item.name);
        }
        FindAndEquipSlot(item);
    }

    public void FindAndEquipSlot(EquipmentObject item)
    {
        EquipmentSlot slot = item.GetSlot();
        PlayerEquipmentSlot[] slots = equipmentPanel.GetComponentsInChildren<PlayerEquipmentSlot>();
        foreach (PlayerEquipmentSlot it in slots)
        {
            if (it.slot == slot)
            {
                Debug.Log("Equipping slot " + slot + " with item: " + item.name);
                it.Equip(item);
            }
        }
    }
    public void FindAndUnEquipSlot(EquipmentObject item)
    {
        EquipmentSlot slot = item.GetSlot();
        PlayerEquipmentSlot[] slots = equipmentPanel.GetComponentsInChildren<PlayerEquipmentSlot>();
        foreach (PlayerEquipmentSlot it in slots)
        {
            if (it.slot == slot)
            {
                it.UnEquip();
            }
        }
    }

    public void UnEquipItem(EquipmentSlot slot)
    {
        EquipmentObject item = GetEquippedItems()[slot];
        if (item != null)
        {
            int itemsCount = inventoryManager.inventory.GetItems().Count;
            if (itemsCount < inventoryManager.inventory.MaxSize)
            {
                FindAndUnEquipSlot(item);
                inventoryManager.inventory.AddItem(new PlayerInventorySlot(item));
                Debug.Log("Items count after unequip " + inventoryManager.inventory.GetItems().Count);
            }
            else
            {
                // TODO: Create general logging UI to user
                Debug.LogError("Cannot unequip item " + item.name + " because your inventory is full!");
            }

        }
        else
        {
            Debug.LogError("Cannot unequip item from slot " + slot + " because there is no item to remove.");
        }
    }

    public Dictionary<EquipmentSlot, EquipmentObject> GetEquippedItems()
    {
        Dictionary<EquipmentSlot, EquipmentObject> items = new();
        PlayerEquipmentSlot[] slots = equipmentPanel.GetComponentsInChildren<PlayerEquipmentSlot>();
        foreach (PlayerEquipmentSlot it in slots)
        {
            if (it.equippedItem != null)
            {
                items.Add(it.slot, it.equippedItem);
            }
        }
        return items;
    }
}
