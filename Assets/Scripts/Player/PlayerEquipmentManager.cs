using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    Dictionary<EquipmentSlot, EquipmentObject> equippedItems = new Dictionary<EquipmentSlot, EquipmentObject>();
    public RectTransform equipmentPanel;
    private PlayerInventoryManager inventoryManager; 

    void Start() {
        inventoryManager = gameObject.transform.parent.GetComponentInChildren<PlayerInventoryManager>();
        if (inventoryManager == null) {
            Debug.LogError("No inventory manager found in sibling. Add inventory manager to a sibling of this object for equipment to work properly.");
        }
        equipmentPanel.gameObject.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            equipmentPanel.gameObject.SetActive(!equipmentPanel.gameObject.activeInHierarchy);
        }
    }

    void EquipItem(EquipmentObject item) {
        EquipmentSlot slot = item.GetSlot();
        if (equippedItems.ContainsKey(slot)) {
            ItemObject currentItem = equippedItems[slot];
            inventoryManager.inventory.RemoveItem(item.name);
            equippedItems[slot] = item;
            inventoryManager.inventory.AddItem(new PlayerInventorySlot(item));
        } else {
            inventoryManager.inventory.RemoveItem(item.name);
            equippedItems.Add(slot, item);
        }
    }

    void UnEquipItem(EquipmentSlot slot) {
        EquipmentObject item = equippedItems[slot];
        if (item != null) {
            if (inventoryManager.inventory.GetItems().Count >= inventoryManager.inventory.MaxSize) {
                equippedItems.Remove(slot);
                inventoryManager.inventory.AddItem(new PlayerInventorySlot(item));
            } else {
                // TODO: Create general logging UI to user
                Debug.LogError("Cannot unequip item " + item.name + " because your inventory is full!");
            }

        } else {
            Debug.LogError("Cannot unequip item from slot " + slot + " because there is no item to remove.");
        }

    }
}
