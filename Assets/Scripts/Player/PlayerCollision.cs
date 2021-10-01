using System.Collections;
using System.Collections.Generic;
using SuperTiled2Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public SceneLoader sceneLoader;
    
    private bool isColliding;

    private const int ITEM_CHECK_RADIUS = 100;
    Camera cam;
    GroundItemManager hitGroundItem = null;


    private void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Update() {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 0.25f, LayerMask.NameToLayer("Ground"));
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("GroundItem")) {
                hitGroundItem = hitCollider.gameObject.GetComponent<GroundItemManager>();
                break;
            }
            else {
                hitGroundItem = null;
            }
        }
        if (hitGroundItem != null) {
            Vector3 screenPos = cam.WorldToScreenPoint(hitGroundItem.transform.position);
            TooltipManager.ShowtoolTip(string.Format("{0} ({1}) \nPick up (space)", hitGroundItem.item.name, hitGroundItem.amount), screenPos);
        } else {
            TooltipManager.HideTooltip();
        }
    }

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