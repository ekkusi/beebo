using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCollision))]
public class PlayerInteraction : MonoBehaviour
{
    IInteractionable interactionTarget = null;

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, LayerMask.NameToLayer("Ground"));
        List<IInteractionable> interactionTargets = new List<IInteractionable>();
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("GroundItem") || hitCollider.gameObject.CompareTag("NPC"))
            {
                interactionTargets.Add(hitCollider.gameObject.GetComponent<IInteractionable>());
            }
        }
        if (interactionTargets.Count <= 0)
        {
            interactionTarget?.StopInteraction();
            interactionTarget = null;
        }
        else if (interactionTargets[0] != interactionTarget)
        {
            Debug.Log("Interaction target changed");
            interactionTarget = interactionTargets[0];
            interactionTarget.ShowInteractionTooltip();
        }
        if (Input.GetKeyDown("space"))
        {
            interactionTarget?.Interact();
        }
    }
}
