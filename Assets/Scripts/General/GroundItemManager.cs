using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GroundItemManager : MonoBehaviour
{
  public ItemObject item;
  public int amount = 1;
  private SpriteRenderer spriteRenderer;
  void OnValidate()
  {
    // Remove error coming when playing
    spriteRenderer = GetComponent<SpriteRenderer>();
    if (spriteRenderer != null && item != null)
    {
      spriteRenderer.sprite = item.sprite;
    }
  }
  void Awake()
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
}