using UnityEngine;

public static class LayerUtils
{
    public static void SetLayerRecursively(GameObject gameObject, int newLayer)
    {

        gameObject.layer = newLayer;

        // foreach (Transform child in gameObject.transform)
        // {
        //     SetLayerRecursively(child.gameObject, newLayer);
        // }
    }
}