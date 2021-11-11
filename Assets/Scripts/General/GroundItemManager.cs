using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GroundItemManager : Interactionable
{
    [SerializeField]
    private ItemObject item;
    public int amount = 1;
    private SpriteRenderer spriteRenderer;
    private PlayerInventoryManager inventoryManager;
    private PhotonView view;

    public override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }
    private void Start()
    {
        view = GetComponent<PhotonView>();
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
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others, CachingOption = EventCaching.AddToRoomCacheGlobal };
        PhotonNetwork.RaiseEvent((byte)CustomEvents.DestroyObject, view.ViewID, raiseEventOptions, SendOptions.SendReliable);
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


    public void SetPlayer(GameObject player)
    {
        inventoryManager = player.GetComponent<PlayerInventoryManager>();
    }
}