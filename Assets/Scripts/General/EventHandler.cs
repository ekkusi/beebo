using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CustomEvents
{
    SceneChange
}
public class EventHandler : MonoBehaviour, IOnEventCallback
{
    public const byte SceneChangeEvent = 1;
    private void OnEnable()
    {
        Debug.Log("Hooking photon callback");
        // PhotonNetwork.AddCallbackTarget(this);
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
    public void OnEvent(EventData photonEvent)
    {
        Debug.Log("Received player loaded level on client: " + PhotonNetwork.LocalPlayer.UserId);
        if (photonEvent.Code == SceneChangeEvent)
        {
            object[] data = (object[])photonEvent.CustomData;

            string newScene = (string)data[5];
            string oldScene = (string)data[4];
            string playerId = (string)data[3];
            Debug.Log("Player id : " + playerId);
            string receiverScene = SceneManager.GetActiveScene().name;
            Debug.Log("Client scene: " + receiverScene + ", sender new scene: " + newScene);
            if (oldScene == receiverScene)
            {
                Debug.Log("Destroying obj: " + playerId);
                Destroy(GameObject.Find(playerId));
            }
            if (newScene == receiverScene)
            {
                Debug.Log("Creating obj: " + playerId);
                GameObject playerObj = GeneralManager.InstantiatePlayer(playerId, (Vector3)data[0], (Quaternion)data[1], (int)data[2]);
            }
        }
    }
}
