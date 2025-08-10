using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("Network Settings")]
    [SerializeField] private AppDeviceType device;

    private const int MAX_PLAYER_COUNT = 4;
    private const string ROOM_NAME = "myRoom";
    public const string HMD_NICKNAME = "hmd";
    //Player hmdPlayer;

    private void Awake()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "kr";
        PhotonNetwork.PhotonServerSettings.DevRegion = "kr";
    }

    void Start()
    {
        PhotonNetwork.NetworkingClient.AppId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime;
        PhotonNetwork.NetworkingClient.AppVersion = Application.version;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.NickName = device.ToString();
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected");
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = MAX_PLAYER_COUNT,
            IsOpen = true,
            IsVisible = true
        };
        PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"failed to join room: error code = {returnCode}, msg = {message}");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.ReconnectAndRejoin();
    }

    public override void OnJoinedRoom()
    {
        //hmdPlayer = PhotonNetwork.PlayerListOthers.FirstOrDefault(p => p.NickName == HMD_NICKNAME);
        Debug.Log("joined room");
    }

    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    if (newPlayer.NickName == HMD_NICKNAME && hmdPlayer == null)
    //        hmdPlayer = newPlayer;
    //}

    //public override void OnPlayerLeftRoom(Player otherPlayer)
    //{
    //    if (otherPlayer.NickName == HMD_NICKNAME && hmdPlayer != null)
    //        hmdPlayer = null;
    //}
}
