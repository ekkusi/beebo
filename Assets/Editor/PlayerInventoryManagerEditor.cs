using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerInventoryPanelManager))]
public class PlayerInventoryPanelEditor : Editor
{
  public override void OnInspectorGUI()
  {
      base.OnInspectorGUI();
      
      PlayerInventoryPanelManager inventoryManager = (PlayerInventoryPanelManager)target;
      inventoryManager.InitializeInventory();

      if (GUILayout.Button("Initialize inventory panel")) { 
          InventoryPanelManager<PlayerInventorySlot> panelManager = inventoryManager.GetComponentInChildren<InventoryPanelManager<PlayerInventorySlot>>();
          for (int i =  0; i < inventoryManager.transform.childCount; i++)
          {
              DestroyImmediate(inventoryManager.transform.GetChild(i).gameObject);
          }
          inventoryManager.CreatePanelContent();
      }
  }
}
