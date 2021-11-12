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
        switch (photonEvent.Code)
        {
            case (byte)CustomEvents.SceneChange:
                object[] data = (object[])photonEvent.CustomData;
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
                int viewId = (int)photonEvent.CustomData;
                PhotonView view = PhotonNetwork.GetPhotonView(viewId);
                if (view != null)
                {
                    Destroy(view.gameObject);
                }
                else
                {
                    Debug.LogError("Obj to destroy not found");
                }
                break;

        }
    }
}
