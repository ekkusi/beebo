using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class InventoryManager<SlotType> : MonoBehaviour where SlotType : InventorySlot
{
    Dictionary<string, GameObject> itemsDisplayed = new Dictionary<string, GameObject>();
    public InventoryObject<SlotType> inventory;
    public RectTransform inventoryPanel;

    public void Awake()
    {
        InitializeInventory();
    }

    public void Start()
    {
        CreatePanelContent();
        if (inventory == null)
        {
            InitializeInventory();
        }
    }

    public void Update()
    {
        UpdatePanelContent();
    }

    public abstract void InitializeInventory();

    public void CreatePanelContent()
    {
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
        List<SlotType> items = inventory.GetItems();
        for (int i = 0; i < items.Count; i++)
        {
            SlotType currentSlot = items[i];
            CreateItemObject(currentSlot);
        }
    }


    public void UpdatePanelContent()
    {
        List<SlotType> items = inventory.GetItems();

        // TODO: This loop runs on every update, could there be a way to run it only when inventory is changed?
        for (int i = 0; i < items.Count; i++)
        {
            SlotType slot = items[i];
            if (slot == null)
            {
                Debug.Log("Removing item, it null");
                RemoveItem(slot.item.name);
                i--;
            }
            else if (slot.amount == 0)
            {
                Debug.Log("Removing item: " + slot.item.name);
                RemoveItem(slot.item.name);
            }
            else if (itemsDisplayed.ContainsKey(slot.item.name))
            {
                if (!slot.item.isSingleSlot)
                {
                    GameObject matchingObj = itemsDisplayed[slot.item.name];
                    TextMeshProUGUI objText = matchingObj.GetComponentInChildren<TextMeshProUGUI>(true);
                    objText.text = slot.amount.ToString("n0");
                    UpdateAmountTextPos(objText.gameObject);
                }
            }
            else
            {
                Debug.Log("Creating new item in update");
                CreateItemObject(slot);
            }
        }
    }

    public void RemoveItem(string itemName)
    {
        GameObject item = itemsDisplayed[itemName];
        inventory.RemoveItem(item.name);
        Destroy(item);
        itemsDisplayed.Remove(item.name);
    }

    public virtual GameObject CreateItemObject(SlotType slot)
    {
        GameObject newObj = new GameObject(slot.item.name);
        Image image = newObj.AddComponent<Image>();
        image.sprite = slot.item.sprite;
        RectTransform newObjTransform = newObj.GetComponent<RectTransform>();
        newObjTransform.SetParent(inventoryPanel.gameObject.transform);
        newObjTransform.localScale = new Vector3(1, 1, 1);
        newObj.SetActive(true);

        if (!slot.item.isSingleSlot)
        {
            GameObject textObj = new GameObject("Amount text");
            TextMeshProUGUI textComponent = textObj.AddComponent<TextMeshProUGUI>();
            textComponent.text = slot.amount.ToString("n0");
            textComponent.alignment = TextAlignmentOptions.BottomRight;
            textComponent.transform.localScale = new Vector3(1, 1, 1);
            textObj.transform.SetParent(newObj.transform);
            textComponent.fontSize = 20;
            textComponent.color = new Color(0, 0, 0, 255);
            ContentSizeFitter contentFitter = textObj.AddComponent<ContentSizeFitter>();
            contentFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            RectTransform textRect = textObj.GetComponent<RectTransform>();
            textRect.localScale = new Vector3(1, 1, 1);
            textRect.anchorMin = new Vector2(1, 0);
            textRect.anchorMax = new Vector2(1, 0);
            textRect.anchoredPosition = new Vector3(-textComponent.preferredWidth / 2, textComponent.preferredHeight / 2, 0);
        }

        itemsDisplayed.Add(slot.item.name, newObj);
        return newObj;
    }

    public void UpdateAmountTextPos(GameObject textObj)
    {
        TextMeshProUGUI textComponent = textObj.GetComponent<TextMeshProUGUI>();
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchoredPosition = new Vector3(-textComponent.preferredWidth / 2, textComponent.preferredHeight / 2, 0);
    }

}
