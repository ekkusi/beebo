using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{
    public GameObject player;
    public Camera customCamera;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(customCamera);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CustomLoadScene(string newScene, Vector3 newPlayerPos)
    {
        SceneManager.LoadScene(newScene);
        player.transform.position = newPlayerPos;
    }
}
