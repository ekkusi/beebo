using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInventoryManager : InventoryManager<PlayerInventorySlot>
{
    [SerializeField]
    private PlayerInventoryObject playerInventory;
    public ItemObject coinsItem;

    public override void InitializeInventory()
    {
        inventory = playerInventory;
    }

    new void Start()
    {
        base.Start();
        CloseInventory();
    }

    new void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel.gameObject.activeInHierarchy)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }

    }

    public override GameObject CreateItemObject(PlayerInventorySlot slot)
    {
        GameObject obj = base.CreateItemObject(slot);
        PlayerInventoryItemManager itemManager = obj.AddComponent<PlayerInventoryItemManager>();
        itemManager.slot = slot;
        return obj;
    }

    public int GetCoinsAmount()
    {
        int amount = 0;
        foreach (PlayerInventorySlot slot in playerInventory.GetItems())
        {
            if (slot.item.name == "Coins")
            {
                amount = slot.amount;
            }
        }
        return amount;
    }

    public void SubstractCoinsAmount(int amount)
    {
        int coinsAmount = 0;
        foreach (PlayerInventorySlot slot in playerInventory.GetItems())
        {
            if (slot.item.name == "Coins")
            {
                coinsAmount = slot.amount;
                break;
            }
        }
        if (amount <= coinsAmount)
        {
            SubstractAmountOrRemove("Coins", amount);
        }
        else
        {
            throw new NotEnoughCoinsException();
        }
    }
    public void AddCoins(int amount)
    {
        try
        {
            AddItem(new PlayerInventorySlot(coinsItem, amount));
        }
        catch (NoSpaceInInventoryException)
        {
            Debug.Log("No space in inventory to add coins");
        }
    }
}
