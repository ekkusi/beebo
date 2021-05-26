using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerCollision collision;
    // Start is called before the first frame update
    void Start()
    {
        collision = GetComponent<PlayerCollision>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
