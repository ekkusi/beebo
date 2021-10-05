using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatNPCManager : Interactionable
{
    public ChatNPCObject npc;
    private SpriteRenderer spriteRenderer;

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

    public override void ShowInteractionTooltip()
    {
        TooltipManager.ShowtoolTip(string.Format("{0} \nSpeak (space)", gameObject.name), positionOnScreen);
    }

    public override void Interact()
    {
        ChatBoxManager.OpenChat(npc);
    }
    public override void StopInteraction()
    {
        ChatBoxManager.CloseChat();
        TooltipManager.HideTooltip();
    }
}
