using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject groundItemPrefab;
    [SerializeField]
    private GameObject playerPrefab;
    public static PrefabManager instance = null;
    public static GameObject GroundItemPrefab { get { return instance?.groundItemPrefab; } }
    public static GameObject PlayerPrefab { get { return instance?.playerPrefab; } }
    void Awake()
    {
        Debug.Log("Setting prefab manager instance");
        if (instance == null)
        {
            instance = this;
        }
    }
}
