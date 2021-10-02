using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{
    public string initialScene = "Fisander";
    public string initialOutDoorwayName = "VillageStart";
    public bool loadInitialScreenOnStart = true;
    private static GeneralManager instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        Destroy(this.gameObject);
    }
    // Start is called before the first frame update

    void Start()
    {
        if (loadInitialScreenOnStart)
        {
            SceneLoader sceneLoader = GetComponent<SceneLoader>();
            sceneLoader.CustomLoadScene(initialScene, initialOutDoorwayName);
        }
    }
}
