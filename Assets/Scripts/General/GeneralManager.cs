using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public string initialScene = "Fisander";
    public string initialOutDoorwayName = "VillageStart";
    public bool loadInitialScreenOnStart = true;
    private static GeneralManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
            return;
        }
        Destroy(this.gameObject);
    }


    public void InitializePlayer()
    {
        Debug.Log("Initializing player");
        GameObject player = InstantiatePlayer(PhotonNetwork.LocalPlayer.UserId, Vector3.zero, Quaternion.identity);
        player.transform.SetParent(transform);
        PhotonView photonView = player.GetComponent<PhotonView>();
        PhotonNetwork.AllocateViewID(photonView);
        PhotonNetwork.LocalPlayer.TagObject = player;
        SceneLoader sceneLoader = GetComponent<SceneLoader>();
        CameraRunner cameraRunner = Camera.main.GetComponent<CameraRunner>();
        sceneLoader.SetPlayer(player);
        cameraRunner.SetPlayerToFollow(player.transform);
        // If not dev build or loadInitialScreenOnStart is set
        if (!Debug.isDebugBuild || loadInitialScreenOnStart)
        {
            sceneLoader.CustomLoadScene(initialScene, initialOutDoorwayName);
        }
        else
        { // Otherwise get pos from first exit doorway in scene
            GameObject[] exitDoorways = GameObject.FindGameObjectsWithTag("ExitDoorway");
            if (exitDoorways.Length > 0)
            {
                player.transform.position = exitDoorways[0].transform.position;
            }
        }
    }

    public static GameObject InstantiatePlayer(string playerId, Vector3 position, Quaternion rotation)
    {
        GameObject player = Instantiate(instance.playerPrefab, position, rotation);
        player.name = playerId;
        return player;
    }

    public static GameObject InstantiatePlayer(string playerId, Vector3 position, Quaternion rotation, int viewId)
    {
        GameObject player = InstantiatePlayer(playerId, position, rotation);
        PhotonView photonView = player.GetComponent<PhotonView>();
        photonView.ViewID = viewId;
        return player;
    }

    public void ClientConnected(ulong clientId)
    {
        Debug.Log("Connected client: " + clientId);

    }
}
