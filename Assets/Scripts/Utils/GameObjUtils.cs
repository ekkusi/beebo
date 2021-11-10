using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjUtils : MonoBehaviour
{

    public static GameObject GetParentByTagNameRecursive(GameObject obj, string tagName)
    {

        GameObject parent = obj.transform.parent.gameObject;
        if (parent != null)
        {
            if (parent.CompareTag(tagName)) return parent;
            else return GetParentByTagNameRecursive(parent, tagName);
        }
        return null;
    }
}