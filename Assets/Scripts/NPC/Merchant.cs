using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour, IInteractionable
{
    public MerchantInventoryObject inventory;


    public void Interact()
    {
        Debug.Log("Interacting with merchant: " + this.name);
    }

    public void StopInteraction()
    {
        TooltipManager.HideTooltip();
    }

    public void ShowInteractionTooltip()
    {
        TooltipManager.ShowtoolTip(string.Format("{0} \nSpeak (space)", gameObject.name));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
