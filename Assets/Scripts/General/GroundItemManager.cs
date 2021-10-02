using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GroundItemManager : MonoBehaviour, IInteractionable
{
    public ItemObject item;
    public int amount = 1;
    private SpriteRenderer spriteRenderer;
    private PlayerInventoryManager inventoryManager;

    void Start()
    {
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

    public void OnMouseEnter()
    {
        TooltipManager.ShowtoolTip(string.Format("{0} ({1}) \n{2}", item.name, amount, item.description));
    }

    public void OnMouseExit()
    {
        TooltipManager.HideTooltip();
    }

    public void Interact()
    {
        inventoryManager.inventory.AddItem(new PlayerInventorySlot(item, amount));
        Destroy(gameObject);
    }
}