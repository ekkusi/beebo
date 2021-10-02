using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRunner : MonoBehaviour
{
    public Transform player;
    public float zoom = 3f;

    void Start()
    {
        Camera.main.orthographicSize = zoom;
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10); // Camera follows the player with specified offset position

        // camera.fieldOfView = 5;
    }
}
