using System.Collections;
using System.Collections.Generic;
using SuperTiled2Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GeneralManager generalManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void OnCollisionEnter(Collision other) {
    //     Debug.Log("Colliding");
    //     Debug.Log(other.transform.name);
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        // SceneManager.LoadScene(other.name);

        switch (other.tag) {
            case "Doorway": {
                SuperCustomProperties collisionProps = other.GetComponent<SuperCustomProperties>();

                float newPosX = float.MinValue;
                float newPosY = float.MinValue;
                foreach (CustomProperty prop in collisionProps.m_Properties) {
                    switch (prop.m_Name) {
                        case "pos_x":
                            newPosX = float.Parse(prop.m_Value) * 32 / 100;
                            break;
                        case "pos_y":
                            newPosY = float.Parse(prop.m_Value) * 32 / 100;
                            break;
                    }
                }
                Debug.Log(newPosX);

                // Ensure that are set
                if (newPosX != float.MinValue && newPosY != float.MinValue) {
                    generalManager.CustomLoadScene(other.name, new Vector3(newPosX, newPosY, 0));
                } else {
                    Debug.LogError("Doorway without pos_x and pos_y properties. Specify them to load to new scene!");
                }
                break;
            }
        }
    }
}