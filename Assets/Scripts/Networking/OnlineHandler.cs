using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OnlineHandler : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject UserPrefab;
    private string className = "aula_1";
    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        joinOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined lobby");
        InstantiatePlayer();
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(className);
    }

    public void joinRoom()
    {
        PhotonNetwork.JoinRoom(className);
    }

    public void joinOrCreateRoom()
    {
        Debug.Log("Joining or creating room");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = false;
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(className, roomOptions, TypedLobby.Default);
    }

    public void InstantiatePlayer()
    {
        PhotonNetwork.Instantiate("User", new Vector3(0, 0, 0), Quaternion.identity);
    }
}
