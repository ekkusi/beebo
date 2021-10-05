using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GroundItemManager : Interactionable
{
    public ItemObject item;
    public int amount = 1;
    private SpriteRenderer spriteRenderer;
    private PlayerInventoryManager inventoryManager;

    new void Start()
    {
        base.Start();
        inventoryManager = GameObject.Find("PlayerInventory").GetComponent<PlayerInventoryManager>();
    }
    void OnValidate()
    {
        // Remove error coming when playing
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && item != null)
        {
            spriteRenderer.sprite = item.sprite;
        }
    }
    public void Awake()
    {
        // This will activate on play, not OnValidate
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        inventoryManager.AddItem(new PlayerInventorySlot(item, amount));
        Destroy(gameObject);
    }
    public override void StopInteraction()
    {
        TooltipManager.HideTooltip();
    }

    public override void ShowInteractionTooltip()
    {
        TooltipManager.ShowtoolTip(string.Format("{0} ({1}) \nPick up (space)", item.name, amount), positionOnScreen);
    }

}