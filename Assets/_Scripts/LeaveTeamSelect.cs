using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

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
        SceneManager.LoadScene("ConnectToMaster");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        DisconnectPlayer();
    }
}
