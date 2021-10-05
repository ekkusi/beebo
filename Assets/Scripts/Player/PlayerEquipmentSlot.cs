using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerEquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    public EquipmentSlot slot;
    [SerializeField]
    private Sprite notEquippedSprite;
    private PlayerEquipmentManager equipmentManager;
    public EquipmentObject equippedItem { get; private set; }
    private bool isHovering;

    void Start()
    {
        equipmentManager = transform.parent.parent.GetComponent<PlayerEquipmentManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (
            EventUtils.IsPointerOverGameObject(gameObject) && equippedItem != null
        )
        {
            isHovering = true;
            string tooltipMsg = equippedItem.ToString("Unequip (click) \n\n");
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
        equipmentManager.UnEquipItem(slot);
        TooltipManager.HideTooltip();
    }
    public void Equip(EquipmentObject item)
    {
        equippedItem = item;
        Image image = GetComponent<Image>();
        image.sprite = item.sprite;
    }

    public void UnEquip()
    {
        equippedItem = null;
        Image image = GetComponent<Image>();
        image.sprite = notEquippedSprite;
    }
}
