using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks //we need to derive from MonoBehaviourPunCallbacks, since we need access to OnConnectedToMaster
{
    void Start()
    {
        //Connects to the Photon Master Server using the settings in the ScriptableObject
        PhotonNetwork.ConnectUsingSettings();       
    }

    //Callback that occurs when we connect to master server, which we did above.
    public override void OnConnectedToMaster()
    {
        Debug.Log("Successfully connected to PUN MasterServer");
        SceneManager.LoadScene("MainMenu");
    }
}
