using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GuestTeamPicker : MonoBehaviour
{
    public RectTransform[] GuestChoicePositions;
    public RectTransform GuestSprite;

    public Button LeftButton;
    public Button RightButton;

    private PhotonView view;
    private ExitGames.Client.Photon.Hashtable playerCustomProps;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        playerCustomProps = GameController.Instance.playerCustomProps;

        if (!PhotonNetwork.IsMasterClient)
        {
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(true);
            //assign this local player the custrom properties if it is the Guest
            PhotonNetwork.LocalPlayer.CustomProperties = playerCustomProps;
        }
        else
        {
            LeftButton.gameObject.SetActive(false);
            RightButton.gameObject.SetActive(false);
        }
    }

    public void MoveLeft()
    {
        if (!PhotonNetwork.IsMasterClient)
        {

            playerCustomProps["TeamChoice2"] = "TeamA";
            playerCustomProps["TeamImage2"] = "Panda";
            view.RPC("moveLeftRPC", RpcTarget.All);
        }
    }

    [PunRPC]
    void moveLeftRPC()
    {   
        //could update game controller here?
        GameController.Instance.Player2Choice.Team = PlayerSelection.TeamSelection.TeamA;
        //Debug.Log("Team when moved p2 left: " + GameController.Instance.Player2Choice.Team);

        GuestSprite.anchoredPosition = GuestChoicePositions[0].anchoredPosition;
        RightButton.gameObject.SetActive(true);
        LeftButton.gameObject.SetActive(false);
        showOnlyMyButtons(true);
    }

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

    public void DisableButtonsAfterConfirmation()
    {
        LeftButton.gameObject.SetActive(false);
        RightButton.gameObject.SetActive(false);
    }
}
