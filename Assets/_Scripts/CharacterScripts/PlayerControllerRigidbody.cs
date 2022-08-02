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
    public TextMeshProUGUI PlayerName; //the text that will hold the nickname
    public Image PlayerImage1; //these two will hold the team image (panda/hippo)
    public Image PlayerImage2;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        //we set the nickname of both players that spawn this way
        if (view.IsMine)
        {
            //if this is me, give me my nickname
            PlayerName.text = PhotonNetwork.NickName;
        }
        else
        {
            //if not, give it to the other player theirs
            PlayerName.text = view.Owner.NickName;
        }

        //some funky code, I admit. But this makes it so that only MY player has the team image.
        //If i made both visible, it was hard to see which was yours and which was the other players'
        if (this.gameObject.name == "Player1(Clone)") //check if the instantiated clone of the prefab has this specific name. it means its the host
        {
            //here I used the custom properties, to check what the player has picked during team select
            //then we update the corret images
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
        //same for the other player (guest)
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
        //here I move the player. view.IsMine ensures I dont move both players, only mine
        if (view.IsMine)
        {
            playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            movePlayer();
        }

    }
    //standard rigidbody apply force vector
    private void movePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput) * Speed;
        PlayerBody.velocity = new Vector3(moveVector.x, PlayerBody.velocity.y, moveVector.z);
    }
}
