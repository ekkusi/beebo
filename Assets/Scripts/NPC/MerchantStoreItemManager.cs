using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MerchantStoreItemManager : MonoBehaviour, IPointerClickHandler // 2
{
    private MerchantStoreManager storeManager;
    public MerchantInventorySlot slot;
    private bool isHovering = false;
    // Start is called before the first frame update
    void Start()
    {
        storeManager = transform.parent.parent.parent.GetComponent<MerchantStoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            EventUtils.IsPointerOverGameObject(gameObject)
        )
        {
            isHovering = true;
            // string message = slot.item.itemType == ItemType.Equipment ? string.Format(string.Format("EQUIP\n{0} \n", slot.item.name)) : slot.item.name;
            string tooltipMsg = slot.item.ToString(string.Format("Buy ({0})\n\n", storeManager.GetBuyPrice(slot.item)));
            TooltipManager.ShowtoolTip(tooltipMsg);
        }
        else if (isHovering)
        {
            isHovering = false;
            TooltipManager.HideTooltip();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        storeManager.BuyItem(slot);
        if (slot.amount <= 0)
        {
            TooltipManager.HideTooltip();
        }
    }
}
