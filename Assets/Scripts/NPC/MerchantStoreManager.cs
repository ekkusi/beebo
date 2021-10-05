using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MerchantStoreManager : InventoryManager<MerchantInventorySlot>
{
    [SerializeField]
    private MerchantInventoryObject merchantInventory;
    public PlayerInventoryManager playerInventoryManager;

    // NOTE: If two stores can be opened at the same time, this needs to be changed from static to something else
    public static MerchantStoreManager activeManager = null;

    public static bool isOpen { get { return activeManager != null; } }
    public override void InitializeInventory()
    {
        inventory = merchantInventory;
    }

    new void Start()
    {
        base.Start();
        inventoryPanel.gameObject.SetActive(false);
    }

    public override void OpenInventory()
    {
        base.OpenInventory();
        activeManager = this;
    }
    public override void CloseInventory()
    {
        base.CloseInventory();
        activeManager = null;
    }

    public override GameObject CreateItemObject(MerchantInventorySlot slot)
    {
        GameObject obj = base.CreateItemObject(slot);
        MerchantStoreItemManager itemManager = obj.AddComponent<MerchantStoreItemManager>();
        itemManager.slot = slot;
        return obj;
    }

    public void BuyItem(MerchantInventorySlot slot, int amount)
    {
        try
        {
            int buyAmount = amount > slot.amount ? slot.amount : amount;
            // This returns exception, if player doesn't have enough coins
            playerInventoryManager.SubstractCoinsAmount(GetBuyPrice(slot.item) * buyAmount);
            playerInventoryManager.AddItem(new PlayerInventorySlot(slot.item, buyAmount));
            SubstractAmountOrRemove(slot.item.name, buyAmount);
        }
        catch (NotEnoughCoinsException)
        {
            Debug.Log(string.Format("Not enough coins for item {0}. Costs {1} and you have {2}", slot.item.name, GetSellPrice(slot.item), playerInventoryManager.GetCoinsAmount()));
        }
    }

    public void BuyItem(MerchantInventorySlot slot)
    {
        BuyItem(slot, 1);
    }

    public void SellItem(MerchantInventorySlot slot)
    {
        AddItem(slot);
        playerInventoryManager.RemoveItem(slot.item.name);
        playerInventoryManager.AddCoins(GetSellPrice(slot.item) * slot.amount);
    }

    public int GetBuyPrice(ItemObject item)
    {
        return (int)Math.Floor(item.basePrice * merchantInventory.buyPriceMultiplier);
    }
    public int GetSellPrice(ItemObject item)
    {
        return (int)Math.Floor(item.basePrice * merchantInventory.sellPriceMultiplier);
    }
}
