using System.Collections;
using System.Collections.Generic;
using SuperTiled2Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public SceneLoader sceneLoader;
    
    private bool isColliding;

    private void OnTriggerEnter2D(Collider2D other) {
        // SceneManager.LoadScene(other.name);

        switch (other.tag) {
            case "EnterDoorway": {
                EnterDoorwayProperties collisionProps = other.GetComponent<EnterDoorwayProperties>();
                sceneLoader.CustomLoadScene(collisionProps.newSceneName, collisionProps.newSceneExitDoorway);
                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
   }

    private void OnCollisionExit2D(Collision2D collision) {
    }
}