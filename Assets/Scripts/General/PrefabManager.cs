using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject groundItemPrefab;
    public static PrefabManager instance = null;
    public static GameObject GroundItemPrefab { get { return instance?.groundItemPrefab; } }
    void Awake()
    {
        Debug.Log("Setting prefab manager instance");
        if (instance == null)
        {
            instance = this;
        }
    }
}
