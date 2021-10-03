using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatNPCManager : MonoBehaviour, IInteractionable
{
    public ChatNPCObject npc;
    private SpriteRenderer spriteRenderer;
    private Camera cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void OnValidate()
    {
        // Remove error coming when playing
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && npc != null)
        {
            spriteRenderer.sprite = npc.sprite;
        }
        if (npc != null)
        {
            gameObject.name = npc.name;
        }
    }

    public void ShowInteractionTooltip()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        TooltipManager.ShowtoolTip(string.Format("{0} \nSpeak (space)", gameObject.name), screenPos);
    }

    public void Interact()
    {
        ChatBoxManager.OpenChat(npc);
    }
    public void StopInteraction()
    {
        ChatBoxManager.CloseChat();
        TooltipManager.HideTooltip();
    }
}
