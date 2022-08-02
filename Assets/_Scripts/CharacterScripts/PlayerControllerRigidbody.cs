using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
public class PlayerControllerRigidbody : MonoBehaviour
{
    Vector3 playerMovementInput;
    PhotonView view;

    [SerializeField]
    private Rigidbody PlayerBody;

    public float Speed;
    public TextMeshProUGUI PlayerName;
    public Image PlayerImage1;
    public Image PlayerImage2;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        if (view.IsMine)
        {
            PlayerName.text = PhotonNetwork.NickName;

        }
        else
        {
            PlayerName.text = view.Owner.NickName;
        }

        if (this.gameObject.name == "Player1(Clone)")
        {
            string animal = (string)PhotonNetwork.LocalPlayer.CustomProperties["TeamImage1"];
            if (animal == "Panda")
            {
                PlayerImage1.gameObject.SetActive(true);
            }
            else if (animal == "Hippo")
            {
                PlayerImage2.gameObject.SetActive(true);
            }
        }        

        if (this.gameObject.name == "Player2(Clone)")
        {
            string animal = (string)PhotonNetwork.LocalPlayer.CustomProperties["TeamImage2"];
            if (animal == "Panda")
            {
                PlayerImage1.gameObject.SetActive(true);
            }
            else if (animal == "Hippo")
            {
                PlayerImage2.gameObject.SetActive(true);
            }
        }
        
    }
    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            movePlayer();
        }

    }

    private void movePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput) * Speed;
        PlayerBody.velocity = new Vector3(moveVector.x, PlayerBody.velocity.y, moveVector.z);
    }
}
