using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MainMenu : MonoBehaviourPunCallbacks
{
    //the inputs that we got from the user
    public TMP_InputField CreateInput;
    public TMP_InputField JoinInput;

    public string LevelToLoad;
    public void CreateRoom()
    {
        //RoomOptions define things like whether is public, or how many players
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true; //if the player uses join random, we need this
        roomOptions.MaxPlayers = 2; //we only support 2 players in this experience

        //Creates a room with the options above, and the name of the input field
        if (CreateInput.text != "")
        {
            PhotonNetwork.CreateRoom(CreateInput.text, roomOptions);
        }
    }

    public void JoinRandomRoom()
    {
        //Joins a random room, if it exists
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRoom()
    {
        //joins a room with the name specified in the input field
        PhotonNetwork.JoinRoom(JoinInput.text);
    }

    public override void OnJoinedRoom()
    {
        //Load the level once joined, use PhotonNetwork not SceneManager,
        //since this is a shared level
        PhotonNetwork.LoadLevel(LevelToLoad);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //base.OnJoinRandomFailed(returnCode, message);
        Debug.LogError("No Rooms Found! Create a Room before joining!");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        //base.OnJoinRoomFailed(returnCode, message);
        Debug.LogError("Can't Connect to room! Make sure the Room name is right," +
            " or that it exists!");
    }
}
