using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class NickNameHandler : MonoBehaviour
{
    public TMPro.TMP_InputField nameInput;

    //here we set the photon nickname to whatever we inputed in the input field
    public void ChangeName()
    {
        PhotonNetwork.NickName = nameInput.text;
    }
}
