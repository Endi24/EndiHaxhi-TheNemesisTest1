using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    public void MoveLeft()
    {

        PlayerSprite.anchoredPosition = PlayerChoicePositions[0].anchoredPosition; //Vector2.Lerp(PlayerSprites[0].anchoredPosition, Player1ChoicePositions[0].anchoredPosition, 0.3f);
        //disable left button - enable right
        //update game manager

    }

    public void MoveRight()
    {

        PlayerSprite.anchoredPosition = PlayerChoicePositions[1].anchoredPosition; //Vector2.Lerp(PlayerSprites[0].anchoredPosition, Player1ChoicePositions[0].anchoredPosition, 0.3f);
        //disable right button - enable left
        //update game manager
        
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
}
