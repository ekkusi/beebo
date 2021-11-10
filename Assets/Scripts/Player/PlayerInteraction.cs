using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PlayerCollision))]
public class PlayerInteraction : MonoBehaviourPun
{
    public Interactionable interactionTarget { get; private set; } = null;

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, LayerMask.NameToLayer("Ground"));
            List<Interactionable> interactionTargets = new List<Interactionable>();
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("GroundItem") || hitCollider.gameObject.CompareTag("NPC"))
                {
                    interactionTargets.Add(hitCollider.gameObject.GetComponent<Interactionable>());
                }
            }
            if (interactionTargets.Count <= 0)
            {
                interactionTarget?.StopInteraction();
                interactionTarget = null;
            }
            else
            {
                interactionTarget = interactionTargets[0];
                interactionTarget.ShowInteractionTooltip();
            }

        }
    }
}
