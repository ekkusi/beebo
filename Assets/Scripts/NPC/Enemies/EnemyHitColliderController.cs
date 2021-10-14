using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitColliderController : MonoBehaviour
{
    private EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = transform.parent.GetComponent<EnemyController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // If collider obj is same as obj inside move trigger collider
        if (GameObject.ReferenceEquals(enemyController.playerInMoveDistance?.gameObject, collider.gameObject))
        {
            enemyController.isPlayerInHitDistance = true;
        }
    }
    void OnTriggerStay2D(Collider2D collider)
    {
        if (!enemyController.isPlayerInHitDistance && GameObject.ReferenceEquals(enemyController.playerInMoveDistance?.gameObject, collider.gameObject))
        {
            enemyController.isPlayerInHitDistance = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // If exiting collider obj is same as obj inside move trigger collider
        if (GameObject.ReferenceEquals(enemyController.playerInMoveDistance?.gameObject, collider.gameObject))
        {
            enemyController.isPlayerInHitDistance = false;
        }
    }
}
