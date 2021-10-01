using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventoryManager : InventoryManager<PlayerInventorySlot>     
{
    [SerializeField]
    private PlayerInventoryObject playerInventory;

    public override void InitializeInventory()
    {
        inventory = playerInventory;
    }

    new void Start() {
        base.Start();
        inventoryPanel.gameObject.SetActive(false);
    }

    new void Update() {
        base.Update();
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeInHierarchy);
        }
    }

    public override GameObject CreateItemObject(PlayerInventorySlot slot) {
        GameObject obj = base.CreateItemObject(slot);
        PlayerInventoryItemManager itemManager = obj.AddComponent<PlayerInventoryItemManager>();
        itemManager.slot = slot;
        return obj;
    }
}
