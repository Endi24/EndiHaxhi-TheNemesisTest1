using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class test_UpdateProperties : MonoBehaviour
{
    public TextMeshProUGUI p1;
    public TextMeshProUGUI p2;


    void Start()
    {
        //p1.text = (string)PhotonNetwork.LocalPlayer.CustomProperties["TeamChoice1"];
        //p2.text = (string)PhotonNetwork.LocalPlayer.CustomProperties["TeamChoice2"];

        p1.text = GameController.Instance.Player1Choice.Team.ToString();
        p2.text = GameController.Instance.Player2Choice.Team.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
