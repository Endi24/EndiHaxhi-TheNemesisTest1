using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class NickNameHandler : MonoBehaviour
{
    public TMPro.TMP_InputField nameInput;

    public void ChangeName()
    {
        PhotonNetwork.NickName = nameInput.text;
    }
}
