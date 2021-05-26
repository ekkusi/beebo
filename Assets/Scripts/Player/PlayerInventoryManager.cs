using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public PlayerInventoryObject inventory;
    private PlayerInventoryPanelManager panelManager;

    void Start() {
        panelManager = GetComponentInChildren<PlayerInventoryPanelManager>(true);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Debug.Log(panelManager.);
            panelManager.gameObject.SetActive(!panelManager.gameObject.activeInHierarchy);
        }
    }
}
