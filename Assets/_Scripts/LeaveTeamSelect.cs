using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

//if i leave during team select, then take me to the match making
public class LeaveTeamSelect : MonoBehaviourPunCallbacks
{
    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectMe());
    }

    IEnumerator DisconnectMe()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene("ConnectToMaster"); //has to restart from the server, because you left the room, and are "serverless" acording to Photon
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        DisconnectPlayer(); //calls the coroutine above, but this is needed to call it form the UI button
    }
}
