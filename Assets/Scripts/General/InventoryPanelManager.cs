using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InventoryPanelManager<SlotType> : MonoBehaviour where SlotType: InventorySlot 
{
    Dictionary<SlotType, GameObject> itemsDisplayed = new Dictionary<SlotType, GameObject>();
    public InventoryObject<SlotType> inventory;

    public void Awake() {
        InitializeInventory();
    }

    public void Start() {
        CreatePanelContent();
    }

    public void Update() {
        Debug.Log("Updating InventorypanelManager");
        UpdatePanelContent();
    }

    public abstract void InitializeInventory();

    public void CreatePanelContent() { 
        for (int i =  0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        List<SlotType> items = inventory.GetItems();
        for (int i = 0; i < items.Count; i++) {
            SlotType currentSlot = items[i];
            GameObject newObj = new GameObject(currentSlot.item.name);
            Image image = newObj.AddComponent<Image>(); 
            image.sprite = currentSlot.item.sprite;
            RectTransform newObjTransform = newObj.GetComponent<RectTransform>();
            newObjTransform.SetParent(gameObject.transform); 
            newObj.SetActive(true);

        }
    }


    public void UpdatePanelContent() {
        // List<SlotType> items = inventory.GetItems();
        // foreach (SlotType item in items) {
        // }
    }
}
