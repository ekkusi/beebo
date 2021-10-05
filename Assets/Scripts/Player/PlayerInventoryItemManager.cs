using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInventoryItemManager : MonoBehaviour, IPointerClickHandler // 2
{
    private PlayerEquipmentManager equipmentManager;
    public PlayerInventorySlot slot;
    private bool isHovering = false;
    // Start is called before the first frame update
    void Start()
    {
        equipmentManager = transform.parent.parent.parent.parent.GetComponentInChildren<PlayerEquipmentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            EventUtils.IsPointerOverGameObject(gameObject)
        )
        {
            isHovering = true;
            MerchantStoreManager activeStoreManager = MerchantStoreManager.activeManager;
            string message = slot.item.itemType == ItemType.Equipment ? slot.item.ToString("Equip (click) \n\n") : slot.item.ToString();
            if (activeStoreManager != null && slot.item.isSellable)
            {
                message = slot.item.ToString(string.Format("Sell ({0}\n\n", activeStoreManager.GetSellPrice(slot.item)));
            }
            TooltipManager.ShowtoolTip(message);
        }
        else if (isHovering)
        {
            isHovering = false;
            TooltipManager.HideTooltip();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked item: " + slot.item.name);
        MerchantStoreManager activeStoreManager = MerchantStoreManager.activeManager;
        if (activeStoreManager != null && slot.item.isSellable)
        {
            Debug.Log("Selling item");
            activeStoreManager.SellItem(new MerchantInventorySlot(slot.item, slot.amount));
        }
        else if (slot.item.itemType == ItemType.Equipment)
        {
            Debug.Log("Equipping item");
            equipmentManager.EquipItem((EquipmentObject)slot.item);
            TooltipManager.HideTooltip();
        }
    }
}
