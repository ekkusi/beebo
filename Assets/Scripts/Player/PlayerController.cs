using System;
using UnityEngine;

[RequireComponent(typeof(PlayerStateManager), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody2D rigidBody;
    private PlayerStateManager stateManager;
    // Start is called before the first frame update

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        stateManager = GetComponent<PlayerStateManager>();
    }

    // Update by physics changes is called once per frame
    void FixedUpdate()
    {
        Vector3 change = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

        if (change != Vector3.zero)
        {
            MovePlayer(change);
            stateManager.ChangeState(PlayerState.HeroWalk);
        }
        else
        {
            stateManager.ChangeState(PlayerState.HeroIdle);
        }
    }

    void MovePlayer(Vector3 change)
    {
        rigidBody.MovePosition(transform.position + Vector3.Normalize(change) * speed * Time.deltaTime);

        // Rotate player based on movement
        float rotateDegrees = (float)Math.Atan2(change.y, change.x) * (float)(360 / (2 * Math.PI));
        transform.eulerAngles = Vector3.forward * rotateDegrees;

    }
}
