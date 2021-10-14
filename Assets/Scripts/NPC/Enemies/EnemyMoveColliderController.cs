using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveColliderController : MonoBehaviour
{
    private EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        enemyController = transform.parent.GetComponent<EnemyController>();
        Debug.Log("Enemy controller: " + enemyController);
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            enemyController.playerInMoveDistance = collider.gameObject.GetComponent<Transform>();
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (!enemyController.playerInMoveDistance && collider.gameObject.CompareTag("Player"))
        {
            enemyController.playerInMoveDistance = collider.gameObject.GetComponent<Transform>();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        // If exit trigger is the same as enemyController player rect, null
        if (GameObject.ReferenceEquals(enemyController.playerInMoveDistance?.gameObject, collider.gameObject))
        {
            enemyController.playerInMoveDistance = null;
        }
    }
}
