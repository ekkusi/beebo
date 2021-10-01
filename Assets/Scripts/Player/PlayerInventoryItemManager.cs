using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInventoryItemManager : MonoBehaviour, IPointerClickHandler // 2
{
    private PlayerEquipmentManager equipmentManager;
    public PlayerInventorySlot slot { get; set; }
    private bool isHovering = false;
    // Start is called before the first frame update
    void Start()
    {
        equipmentManager = transform.parent.parent.parent.GetComponentInChildren<PlayerEquipmentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (
            EventUtils.IsPointerOverGameObject(gameObject)
        ) {
            isHovering = true;
            string message = slot.item.itemType == ItemType.Equipment ? string.Format(string.Format("EQUIP\n{0} \n", slot.item.name)) : slot.item.name;
            TooltipManager.ShowtoolTip(message);
        } else if (isHovering) {
            isHovering = false;
            TooltipManager.HideTooltip();
        }
    }

  public void OnPointerClick(PointerEventData eventData) {
      Debug.Log("Clicked");
      if (slot.item.itemType == ItemType.Equipment) {
        equipmentManager.EquipItem((EquipmentObject)slot.item);
        TooltipManager.HideTooltip();
      }
  }

  public void OnMouseExit()
  {
  }
}
