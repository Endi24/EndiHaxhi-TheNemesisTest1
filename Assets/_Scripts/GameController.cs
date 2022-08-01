using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : GenericSingletonClass<GameController>
{
    public PlayerSelection Player1Choice;
    public PlayerSelection Player2Choice;

    public ExitGames.Client.Photon.Hashtable playerCustomProps = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        Player1Choice = new PlayerSelection();
        Player2Choice = new PlayerSelection();

        Player1Choice.Team = PlayerSelection.TeamSelection.Unselected;
        Player1Choice.PlayerPoints = 0;
        Player2Choice.Team = PlayerSelection.TeamSelection.Unselected;
        Player2Choice.PlayerPoints = 0;
    }
}

public class PlayerSelection
{
    public TeamSelection Team;
    public string CharacterName;
    public int PlayerPoints;

    public enum TeamSelection
    {
        TeamA,
        TeamB,
        Unselected
    }
}
