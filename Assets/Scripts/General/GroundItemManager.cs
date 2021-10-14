using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GroundItemManager : Interactionable
{
    [SerializeField]
    private ItemObject item;
    public int amount = 1;
    private SpriteRenderer spriteRenderer;
    private PlayerInventoryManager inventoryManager;

    public override void Awake()
    {
        base.Awake();
        Debug.Log("AWaking ground item");
        spriteRenderer = GetComponent<SpriteRenderer>();
        inventoryManager = GameObject.Find("Player").GetComponent<PlayerInventoryManager>();
        UpdateSprite();
    }
    void OnValidate()
    {
        // Remove error coming when playing
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
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

    public static void InstantiateGroundItem(ItemObject item, Vector3 position, int amount = 1)
    {
        GameObject obj = Instantiate(PrefabManager.GroundItemPrefab, position, Quaternion.identity);
        obj.name = item.name;
        GroundItemManager itemManager = obj.GetComponent<GroundItemManager>();
        itemManager.item = item;
        itemManager.amount = amount;
        itemManager.UpdateSprite();
    }

    public void UpdateSprite()
    {
        if (spriteRenderer != null && item != null)
        {
            spriteRenderer.sprite = item.sprite;
        }
    }
}