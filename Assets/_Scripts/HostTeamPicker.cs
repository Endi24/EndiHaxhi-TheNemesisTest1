using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HostTeamPicker : MonoBehaviour
{
    public GameObject WaitingForGuest;
    public RectTransform[] HostChoicePositions;
    public RectTransform HostSprite;

    public Button LeftButton;
    public Button RightButton;

    private PhotonView view;
    private ExitGames.Client.Photon.Hashtable playerCustomProps;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        playerCustomProps = GameController.Instance.playerCustomProps;

        if (PhotonNetwork.IsMasterClient)
        {
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(true);

            //assign this local player the custrom properties if it is the Master
            PhotonNetwork.LocalPlayer.CustomProperties = playerCustomProps;
        }
        else
        {
            LeftButton.gameObject.SetActive(false);
            RightButton.gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            //Debug.Log("Guest Joined!");
            WaitingForGuest.gameObject.SetActive(false);
        }
    }

    public void MoveLeft()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerCustomProps["TeamChoice1"] = "TeamA";
            view.RPC("moveLeftRPC", RpcTarget.All);
        }
        
    }

    [PunRPC]
    void moveLeftRPC()
    {
        //could update game controller here?
        GameController.Instance.Player1Choice.Team = PlayerSelection.TeamSelection.TeamA;
        //Debug.Log("Team when moved p1 left: " + GameController.Instance.Player1Choice.Team);

        //Change the position of the Sprite
        HostSprite.anchoredPosition = HostChoicePositions[0].anchoredPosition;
        RightButton.gameObject.SetActive(true);
        LeftButton.gameObject.SetActive(false);
        showOnlyMyButtons(true);
    }

    public void MoveRight()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerCustomProps["TeamChoice1"] = "TeamB";
            view.RPC("moveRightRPC", RpcTarget.All);
        }
        
    }

    [PunRPC]
    void moveRightRPC()
    {
        //could update game controller here?
        GameController.Instance.Player1Choice.Team = PlayerSelection.TeamSelection.TeamB;
        //Debug.Log("Team when moved p1 right: " + GameController.Instance.Player1Choice.Team);

        //Change the position of the Sprite
        HostSprite.anchoredPosition = HostChoicePositions[1].anchoredPosition;
        RightButton.gameObject.SetActive(false);
        LeftButton.gameObject.SetActive(true);
        showOnlyMyButtons(false);
    }

    private void showOnlyMyButtons(bool isMovingLeft)
    {
        if (isMovingLeft == true)
        {
            if (PhotonNetwork.IsMasterClient)
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
            if (PhotonNetwork.IsMasterClient)
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
