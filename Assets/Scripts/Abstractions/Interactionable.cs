using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Interactionable : MonoBehaviour
{
    public abstract void Interact();
    public abstract void ShowInteractionTooltip();
    public abstract void StopInteraction();
    protected Camera cam;
    public Vector3 positionOnScreen
    {
        get
        {

            return cam.WorldToScreenPoint(transform.position);
        }
    }

    public virtual void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

}
