using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//Not used. Left because I was referencing this when I built HostTeamPicker and GuestTeamPicker
public class TeamPicker : MonoBehaviour
{
    public enum PlayerNumber
    {
        Player1,
        Player2
    }

    public PlayerNumber playerNumber;

    public RectTransform[] PlayerChoicePositions;
    public RectTransform PlayerSprite;

    public Button LeftButton;
    public Button RightButton;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playerNumber = PlayerNumber.Player1;
        }
    }

    public void MoveLeft()
    {
        PlayerSprite.anchoredPosition = PlayerChoicePositions[0].anchoredPosition; //Vector2.Lerp(PlayerSprites[0].anchoredPosition, Player1ChoicePositions[0].anchoredPosition, 0.3f);
        //disable left button - enable right
        RightButton.gameObject.SetActive(true);
        LeftButton.gameObject.SetActive(false);
        //update game manager
        if (playerNumber == PlayerNumber.Player1)
        {
            GameController.Instance.Player1Choice.Team = PlayerSelection.TeamSelection.TeamA;
        } 
        else if(playerNumber == PlayerNumber.Player2)
        {
            GameController.Instance.Player2Choice.Team = PlayerSelection.TeamSelection.TeamA;
        }
        ValidateTeamChoice();
    }

    public void MoveRight()
    {
        PlayerSprite.anchoredPosition = PlayerChoicePositions[1].anchoredPosition; //Vector2.Lerp(PlayerSprites[0].anchoredPosition, Player1ChoicePositions[0].anchoredPosition, 0.3f);
        //disable right button - enable left
        RightButton.gameObject.SetActive(false);
        LeftButton.gameObject.SetActive(true);
        //update game manager
        if (playerNumber == PlayerNumber.Player1)
        {
            GameController.Instance.Player1Choice.Team = PlayerSelection.TeamSelection.TeamB;
        }
        else if (playerNumber == PlayerNumber.Player2)
        {
            GameController.Instance.Player2Choice.Team = PlayerSelection.TeamSelection.TeamB;
        }
        ValidateTeamChoice();
    }

    private bool ValidateTeamChoice()
    {
        GameController gm = GameController.Instance;
        if (gm.Player1Choice.Team != gm.Player2Choice.Team)
        {
            if (gm.Player1Choice.Team != PlayerSelection.TeamSelection.Unselected &&
                gm.Player2Choice.Team != PlayerSelection.TeamSelection.Unselected)
            {
                Debug.Log($"Player1 Team: {gm.Player1Choice.Team}; Player 2 Team: " +
                    $"{gm.Player2Choice.Team}");
                return true;
            }
        }
        Debug.Log($"Player1 Team: {gm.Player1Choice.Team}; Player 2 Team: " +
                    $"{gm.Player2Choice.Team}");
        return false; 
    }

    public void SubmitChoices()
    {

    }

    private void ToggleButtonActivation(PlayerNumber pNumber)
    {
        if (pNumber == PlayerNumber.Player1)
        {
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(true);
        }
        else if (pNumber == PlayerNumber.Player2)
        {
            LeftButton.gameObject.SetActive(false);
            RightButton.gameObject.SetActive(false);
        }
    }
}
