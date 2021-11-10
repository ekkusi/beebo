using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private GameObject player;
    private string oldScene;
    private string currentScene;
    private string nextExitDoorwayName;
    private bool loadingScene = false;
    public void CustomLoadSceneStart(string newScene, string sceneExitDoorwayName)
    {
        nextExitDoorwayName = sceneExitDoorwayName;
        Debug.Log("Loading (start) scene: " + newScene);
        OnSceneLoaded(SceneManager.GetSceneByName(newScene), LoadSceneMode.Single);
    }
    public void CustomLoadScene(string newScene, string sceneExitDoorwayName)
    {
        if (!loadingScene)
        {
            loadingScene = true;
            oldScene = SceneManager.GetActiveScene().name;
            currentScene = newScene;
            nextExitDoorwayName = sceneExitDoorwayName;
            PhotonNetwork.LoadLevel(SceneUtil.GetSceneIndexByName(newScene));
        }

        // SceneManager.LoadScene(newScene, LoadSceneMode.Single);
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
        loadingScene = false;
        if (player != null)
        {
            if (nextExitDoorwayName != null && player != null)
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

            GameObject[] groundItems = GameObject.FindGameObjectsWithTag("GroundItem");
            foreach (GameObject obj in groundItems)
            {
                GroundItemManager itemManager = obj.GetComponent<GroundItemManager>();
                itemManager.SetPlayer(player);
            }
        }

        if (player != null)
        {
            PhotonView playerView = player.GetComponent<PhotonView>();
            object[] data = new object[]
           {
              player.transform.position, player.transform.rotation, playerView.ViewID, PhotonNetwork.LocalPlayer.UserId, oldScene, currentScene
           };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others,
            };


            PhotonNetwork.RaiseEvent((byte)CustomEvents.SceneChange, data, raiseEventOptions, SendOptions.SendReliable);
            // PhotonView view = player.GetComponent<PhotonView>();
            // view.RPC("PlayerLoadedLevel", RpcTarget.Others, PhotonNetwork.LocalPlayer, oldScene, currentScene);
        }
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
