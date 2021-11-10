using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField createInput;
    [SerializeField]
    private TMP_InputField joinInput;
    [SerializeField]
    private byte roomSize = 4;
    [SerializeField]
    private GeneralManager generalManager;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartAsHost()
    {
        Debug.Log("Creating room: " + createInput.text);
        PhotonNetwork.CreateRoom(createInput.text, new RoomOptions { MaxPlayers = roomSize });
    }

    public void StartAsClient()
    {
        Debug.Log("Joining room: " + joinInput.text);
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    // public override void OnJoinedRoom()
    // {
    //     Debug.Log("Joined room, should call injitialize player");
    //     generalManager.InitializePlayer();
    // }
}
