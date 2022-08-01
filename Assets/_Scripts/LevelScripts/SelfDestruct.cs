using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SelfDestruct : MonoBehaviour
{
    public void DestroyMeWhenPointScored()
    {
        Invoke("DestroyWithDelay", 0.5f);
    }

    private void DestroyWithDelay()
    {
        Debug.Log($"Destroying {this.gameObject.name}");
        PhotonNetwork.Destroy(this.gameObject);
    }
}
