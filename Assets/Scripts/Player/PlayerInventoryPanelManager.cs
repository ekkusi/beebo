using UnityEngine;

public class PlayerInventoryPanelManager : InventoryPanelManager<PlayerInventorySlot>
{
    [SerializeField]
    private PlayerInventoryObject playerInventory;

    public override void InitializeInventory()
    {
        inventory = playerInventory;
    }

    new void Start() {
        base.Start();
        gameObject.SetActive(false);
    }
}
