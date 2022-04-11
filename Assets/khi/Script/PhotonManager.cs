using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public string gameVersion = "1.0";
    public string nickName = "Unyeon";
    Player myPlayer;

    Vector3 SpawnPos;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        OnLogin();
        SpawnPos = GameObject.Find("player").transform.position;
    }

    void OnLogin()
    {
        PhotonNetwork.GameVersion = this.gameVersion;
        PhotonNetwork.NickName = this.nickName;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("���� ����");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("�� ���忡 �����ϼ̽��ϴ�.");
        this.CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        if(PhotonNetwork.Instantiate("player", Vector3.zero, Quaternion.identity) == null)
        {
            Debug.Log("����?");
        }
        Debug.Log("Joined Room");
    }

    void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 15 });
    }
}