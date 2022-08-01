using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScoredHandler : MonoBehaviour
{
    private PhotonView view;
    private SelfDestruct[] selfDestructs;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        selfDestructs = FindObjectsOfType<SelfDestruct>();
    }

    public void DestroyRelevantObjects()
    {
        view.RPC("DestroyRelevantObjectsRPC", RpcTarget.All);
    }

    [PunRPC]
    public void DestroyRelevantObjectsRPC()
    {
        //Time.timeScale = 0.01f;
        foreach (SelfDestruct item in selfDestructs)
        {
            item.DestroyMeWhenPointScored();
        }
        Debug.Log("Destroying the objects!");
    }
}
