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
        if (Input.GetKeyDown(KeyCode.B))
        {
            inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeInHierarchy);
        }
    }
}
