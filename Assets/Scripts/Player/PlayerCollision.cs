using System.Collections;
using System.Collections.Generic;
using SuperTiled2Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private SceneLoader sceneLoader;

    private const int ITEM_CHECK_RADIUS = 100;

    void Start()
    {
        sceneLoader = GameObject.Find("DDOL").GetComponent<SceneLoader>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // SceneManager.LoadScene(other.name);

        switch (other.tag)
        {
            case "EnterDoorway":
                {
                    EnterDoorwayProperties collisionProps = other.GetComponent<EnterDoorwayProperties>();
                    sceneLoader.CustomLoadScene(collisionProps.newSceneName, collisionProps.newSceneExitDoorway);
                    break;
                }
        }
    }
}