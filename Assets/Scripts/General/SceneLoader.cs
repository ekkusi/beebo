using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  public GameObject player;
  private string nextExitDoorwayName;
  public void CustomLoadScene(string newScene, string sceneExitDoorwayName)
  {
    nextExitDoorwayName = sceneExitDoorwayName;
    SceneManager.LoadScene(newScene);
    // player.transform.position = newPlayerPos;
  }

  void OnEnable()
  {
    SceneManager.sceneLoaded += OnSceneLoaded;
  }

  void OnDisable()
  {
    SceneManager.sceneLoaded -= OnSceneLoaded;
  }

  void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    if (nextExitDoorwayName != null)
    {
      GameObject[] exitDoorways = GameObject.FindGameObjectsWithTag("ExitDoorway");
      Vector3? newPos = null;
      foreach (GameObject exitDoorway in exitDoorways)
      {
        if (exitDoorway.name == nextExitDoorwayName)
        {
          newPos = exitDoorway.transform.position;
        }
      }
      if (newPos != null)
      {
        player.transform.position = (Vector3)newPos;
      }
      else
      {
        Debug.LogError("No matching exit door found in scene: " + scene.name + ". You should add a ExitDoorway and give it's name to the EnterDoorway you're loading this scene from.");
      }
    }
  }
}
