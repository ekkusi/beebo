using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GroundItemManager : MonoBehaviour, IInteractionable
{
    public ItemObject item;
    public int amount = 1;
    private Camera cam;
    private SpriteRenderer spriteRenderer;
    private PlayerInventoryManager inventoryManager;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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

    // public void OnMouseEnter()
    // {
    //     ShowInteractionTooltip();
    // }

    // public void OnMouseExit()
    // {
    //     HideInteractionToolip();
    // }

    public void Interact()
    {
        inventoryManager.AddItem(new PlayerInventorySlot(item, amount));
        Destroy(gameObject);
    }
    public void StopInteraction()
    {
        TooltipManager.HideTooltip();
    }

    public void ShowInteractionTooltip()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        TooltipManager.ShowtoolTip(string.Format("{0} ({1}) \nPick up (space)", item.name, amount), screenPos);
    }

}