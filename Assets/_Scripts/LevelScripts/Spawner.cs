using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawner : MonoBehaviour
{
    public Transform Player1SpawnPosition;
    public Transform Player2SpawnPosition;

    public float minX1, minY1, maxX1, maxY1;
    public float minX2, minY2, maxX2, maxY2;

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
           if (PhotonNetwork.IsMasterClient)
            {
                SpawnPlayer1();
                SpawnBall();
                SpawnGoal1();
                SpawnGoal2();
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
    public void SpawnBall()
    {
        PhotonNetwork.Instantiate("Ball", new Vector3(0, 0.85f, 0), Quaternion.identity);
    }
    public void SpawnGoal1()
    {
        //possibly assign a color
        Vector3 randomPosition = new Vector3(Random.Range(minX1, maxX1), 0.7f, Random.Range(minY1, maxY1));
        PhotonNetwork.Instantiate("Goal1", randomPosition, Quaternion.identity);
    }

    public void SpawnGoal2()
    {
        //possibly assign a color
        Vector3 randomPosition = new Vector3(Random.Range(minX2, maxX2), 0.7f, Random.Range(minY2, maxY2));
        PhotonNetwork.Instantiate("Goal2", randomPosition, Quaternion.identity);
    }
}
