using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class InventoryManager<SlotType> : MonoBehaviour where SlotType: InventorySlot 
{
    Dictionary<string, GameObject> itemsDisplayed = new Dictionary<string, GameObject>();
    public InventoryObject<SlotType> inventory;
    public RectTransform inventoryPanel;

    public void Awake() {
        InitializeInventory();
    }

    public void Start() {
        CreatePanelContent();
    }

    public void Update() {
        UpdatePanelContent();
    }

    public abstract void InitializeInventory();

    public void CreatePanelContent() { 
        for (int i =  0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
        List<SlotType> items = inventory.GetItems();
        for (int i = 0; i < items.Count; i++) {
            SlotType currentSlot = items[i];
            CreateItemObject(currentSlot);
        }
    }


    public void UpdatePanelContent() {
        List<SlotType> items = inventory.GetItems();

        foreach (SlotType slot in items) {
            if (slot.amount == 0) { 
                inventory.RemoveItem(slot.item.name);
                itemsDisplayed.Remove(slot.item.name);
            }
            else if (itemsDisplayed.ContainsKey(slot.item.name)) {
                if (!slot.item.isSingleSlot) {
                    GameObject matchingObj = itemsDisplayed[slot.item.name];
                    TextMeshProUGUI objText = matchingObj.GetComponentInChildren<TextMeshProUGUI>(true);
                    objText.text = slot.amount.ToString("n0");
                }
            } else {
                CreateItemObject(slot); 
            }
        }
    }

    public void CreateItemObject(SlotType slot) {
        GameObject newObj = new GameObject(slot.item.name);
        Image image = newObj.AddComponent<Image>(); 
        image.sprite = slot.item.sprite;
        RectTransform newObjTransform = newObj.GetComponent<RectTransform>();
        newObjTransform.SetParent(inventoryPanel.gameObject.transform); 
        newObj.SetActive(true);

        if (!slot.item.isSingleSlot) {
            GameObject textObj = new GameObject("Amount text");
            TextMeshProUGUI textComponent = textObj.AddComponent<TextMeshProUGUI>();
            textComponent.text = slot.amount.ToString("n0");
            textComponent.alignment = TextAlignmentOptions.BottomRight;
            textObj.transform.SetParent(newObj.transform);
            textComponent.fontSize = 26;
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.sizeDelta = newObj.GetComponent<RectTransform>().sizeDelta;
        }

        itemsDisplayed.Add(slot.item.name, newObj);
    }
}
