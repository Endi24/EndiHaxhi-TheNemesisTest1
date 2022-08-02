using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : GenericSingletonClass<GameController>
{
    //The GameController script has some persistant data, which we use to access throughout the game's lifespan.
    //the team choice or player score for example.
    public PlayerSelection Player1Choice;
    public PlayerSelection Player2Choice;

    //this is a declaration for Photon Custom Properties. I have declared it here, so I can use it elsewhere without access restrictions
    public ExitGames.Client.Photon.Hashtable playerCustomProps = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        //we have to instantiate this way. The class PlayerSelection doesn't derive from Monobehaviour, so we can't
        //assign values in the inspector.
        Player1Choice = new PlayerSelection();
        Player2Choice = new PlayerSelection();

        //default to 0 or Unselected
        Player1Choice.Team = PlayerSelection.TeamSelection.Unselected;
        Player1Choice.PlayerPoints = 0;
        Player2Choice.Team = PlayerSelection.TeamSelection.Unselected;
        Player2Choice.PlayerPoints = 0;
    }
}

//Data holder class that we will use during the lifespan of the game
public class PlayerSelection
{
    public TeamSelection Team; //basically has info regarding which team you will pick later on. 
    public string CharacterName; //not used elsewhere
    public int PlayerPoints; //used extensively when we determine who won the game

    //enum that we will need to check which team we picked. especially validating if a player has picked a team or not, before entering a game
    public enum TeamSelection
    {
        TeamA,
        TeamB,
        Unselected
    }
}
