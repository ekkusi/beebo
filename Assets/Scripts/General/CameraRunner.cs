using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    private Transform player;
    public float zoom = 3f;

    void Start()
    {
        Camera.main.orthographicSize = zoom;
    }

    void Update()
    {
        if (player)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10); // Camera follows the player with specified offset position
        }
        // camera.fieldOfView = 5;
    }

    public void SetPlayerToFollow(Transform player)
    {
        this.player = player;
    }
}
