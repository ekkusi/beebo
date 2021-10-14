using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerInventoryManager))]
public class PlayerInventoryManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerInventoryManager inventoryManager = (PlayerInventoryManager)target;
        inventoryManager.InitializeInventory();

        if (GUILayout.Button("Initialize inventory panel"))
        {
            RectTransform inventoryPanel = inventoryManager.inventoryPanel;
            for (int i = 0; i < inventoryPanel.transform.childCount; i++)
            {
                DestroyImmediate(inventoryPanel.transform.GetChild(i).gameObject);
            }
            inventoryManager.CreatePanelContent();
        }
    }
}
