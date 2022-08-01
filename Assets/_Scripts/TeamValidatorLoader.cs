using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TeamValidatorLoader : MonoBehaviour
{
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        if (PhotonNetwork.IsMasterClient)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        view.RPC("LoadGameRPC", RpcTarget.All);
    }

    [PunRPC]
    void LoadGameRPC()
    {
        PhotonNetwork.LoadLevel("InGame2D");
    }
}
