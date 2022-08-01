using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public Transform Player1SpawnPosition;
    public Transform Player2SpawnPosition;

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
           if (PhotonNetwork.IsMasterClient)
            {
                SpawnPlayer1();
            }
            if (!PhotonNetwork.IsMasterClient)
            {
                SpawnPlayer2();
            }
        }
    }
    public void SpawnPlayer1()
    {
        PhotonNetwork.Instantiate("Player1", Player1SpawnPosition.position, Quaternion.identity);
    }

    public void SpawnPlayer2()
    {
        PhotonNetwork.Instantiate("Player2", Player2SpawnPosition.position, Quaternion.identity);
    }
}
