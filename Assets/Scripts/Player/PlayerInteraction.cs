using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCollision))]
public class PlayerInteraction : MonoBehaviour
{
    public Interactionable interactionTarget { get; private set; }= null;

    // Update is called once per frame
    void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, LayerMask.NameToLayer("Ground"));
        List<Interactionable> interactionTargets = new List<Interactionable>();
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("GroundItem") || hitCollider.gameObject.CompareTag("NPC"))
            {
                interactionTargets.Add(hitCollider.gameObject.GetComponent<Interactionable>());
                Debug.Log("Hit interaction collider");
            }
        }
        if (interactionTargets.Count <= 0)
        {
            interactionTarget?.StopInteraction();
            interactionTarget = null;
        }
        else 
        {
            Debug.Log("Interaction target changed");
            interactionTarget = interactionTargets[0];
            interactionTarget.ShowInteractionTooltip();
        }
    }
}
