using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        //Connects to the Photon Master Server using the settings in the ScriptableObject
        PhotonNetwork.ConnectUsingSettings();       
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Successfully connected to PUN MasterServer");
        SceneManager.LoadScene("MainMenu");
    }
}
