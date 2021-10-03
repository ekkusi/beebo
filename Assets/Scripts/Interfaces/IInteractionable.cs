using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IInteractionable
{
    void Interact();
    void ShowInteractionTooltip();
    void StopInteraction();
}
