using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CustomEvents
{
    SceneChange,
    DestroyObject
}
public class EventHandler : MonoBehaviour, IOnEventCallback
{
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
    public void OnEvent(EventData photonEvent)
    {
        object[] data = (object[])photonEvent.CustomData;
        switch (photonEvent.Code)
        {
            case (byte)CustomEvents.SceneChange:

                string newScene = (string)data[5];
                string oldScene = (string)data[4];
                string playerId = (string)data[3];
                string receiverScene = SceneManager.GetActiveScene().name;
                if (oldScene == receiverScene)
                {
                    Destroy(GameObject.Find(playerId));
                }
                if (newScene == receiverScene)
                {
                    GeneralManager.InstantiatePlayer(playerId, (Vector3)data[0], (Quaternion)data[1], (int)data[2]);
                }
                break;
            case (byte)CustomEvents.DestroyObject:
                Debug.Log("Received message to destroy obj");
                if (PhotonNetwork.IsMasterClient)
                {
                    PhotonView view = PhotonNetwork.GetPhotonView((int)data[0]);
                    if (view != null)
                    {
                        Debug.Log("Destroying obj: " + view.ViewID);
                        PhotonNetwork.Destroy(view);
                    }
                }
                break;

        }
    }
}
