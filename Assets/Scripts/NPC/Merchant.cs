using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : Interactionable
{
    private MerchantStoreManager storeManager;

    new void Start()
    {
        base.Start();
        storeManager = transform.Find("Merchant UI").GetComponentInChildren<MerchantStoreManager>();
    }

    public override void Interact()
    {
        storeManager.OpenInventory();
        TooltipManager.HideTooltip();
    }

    public override void StopInteraction()
    {
        TooltipManager.HideTooltip();
        storeManager.CloseInventory();
    }

    public override void ShowInteractionTooltip()
    {
        TooltipManager.ShowtoolTip(string.Format("{0} \nOpen shop (space)", gameObject.name), positionOnScreen);
    }
}
