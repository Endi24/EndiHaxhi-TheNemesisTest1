using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//this is the team picker for the guest
public class GuestTeamPicker : MonoBehaviour
{
    public RectTransform[] GuestChoicePositions; //ui positions for when the object moves
    public RectTransform GuestSprite;

    //reference to the buttons
    public Button LeftButton; 
    public Button RightButton;

    private PhotonView view; //has to be a photon view, its shared
    private ExitGames.Client.Photon.Hashtable playerCustomProps; //a declaration/reference to the guest custom properties

    private void Start()
    {
        view = GetComponent<PhotonView>();
        playerCustomProps = GameController.Instance.playerCustomProps; //get the custom properties from GameController

        //if this is NOT the master client, then activate the controls. We are in the middle initially
        if (!PhotonNetwork.IsMasterClient)
        {
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(true);
            //assign this local player the custrom properties if it is the Guest
            PhotonNetwork.LocalPlayer.CustomProperties = playerCustomProps;
        }
        else
        {
            //otherwise, this is the host. deactivate the buttons for them
            LeftButton.gameObject.SetActive(false);
            RightButton.gameObject.SetActive(false);
        }
    }

    public void MoveLeft()
    {
        //if this is the guest, call the rpc which will move it for both players
        if (!PhotonNetwork.IsMasterClient)
        {
            playerCustomProps["TeamChoice2"] = "TeamA"; //we dont use this later, we use game controller
            playerCustomProps["TeamImage2"] = "Panda"; //we use this on the game level, to determine which choice was made for the team (panda/hippo)
            view.RPC("moveLeftRPC", RpcTarget.All); //calls the rpc
        }
    }

    [PunRPC]
    void moveLeftRPC()
    {   
        //could update game controller here?
        GameController.Instance.Player2Choice.Team = PlayerSelection.TeamSelection.TeamA;
        //Debug.Log("Team when moved p2 left: " + GameController.Instance.Player2Choice.Team);

        GuestSprite.anchoredPosition = GuestChoicePositions[0].anchoredPosition; //move to position 0 (the left)
        RightButton.gameObject.SetActive(true); //activate buttons as needed
        LeftButton.gameObject.SetActive(false);
        showOnlyMyButtons(true); //make sure only the guest sees the buttons
    }
    //same thing as above, but for the right choice
    public void MoveRight()
    {
        if (!PhotonNetwork.IsMasterClient)
        {

            playerCustomProps["TeamChoice2"] = "TeamB";
            playerCustomProps["TeamImage2"] = "Hippo";
            view.RPC("moveRightRPC", RpcTarget.All);
        }
    }

    [PunRPC]
    void moveRightRPC()
    {
        //could update game controller here?
        GameController.Instance.Player2Choice.Team = PlayerSelection.TeamSelection.TeamB;
        //Debug.Log("Team when moved p2 right: " + GameController.Instance.Player2Choice.Team);

        GuestSprite.anchoredPosition = GuestChoicePositions[1].anchoredPosition;
        RightButton.gameObject.SetActive(false);
        LeftButton.gameObject.SetActive(true);
        showOnlyMyButtons(false);
    }

    //makes sure that only you see the buttons, the guest. the host cannot see yours.
    private void showOnlyMyButtons(bool isMovingLeft)
    {
        if (isMovingLeft == true)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                LeftButton.gameObject.SetActive(false);
                RightButton.gameObject.SetActive(true);
            }
            else
            {
                LeftButton.gameObject.SetActive(false);
                RightButton.gameObject.SetActive(false);
            }
        }
        else if (isMovingLeft == false)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                LeftButton.gameObject.SetActive(true);
                RightButton.gameObject.SetActive(false);
            }
            else
            {
                LeftButton.gameObject.SetActive(false);
                RightButton.gameObject.SetActive(false);
            }
        }
    }

    //after i confirm my choice, i cant change the selection
    public void DisableButtonsAfterConfirmation()
    {
        LeftButton.gameObject.SetActive(false);
        RightButton.gameObject.SetActive(false);
    }
}
