using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventUtils : MonoBehaviour
{

    public static bool IsPointerOverGameObject(GameObject gameObject)
    {
        PointerEventData eventData = new(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults.Exists(x => x.gameObject == gameObject);
    }
}
