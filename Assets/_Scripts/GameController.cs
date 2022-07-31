using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : GenericSingletonClass<GameController>
{
    public PlayerSelection Player1Choice;
    public PlayerSelection Player2Choice;
}

public class PlayerSelection
{
    public TeamSelection Team;
    public string CharacterName;

    public enum TeamSelection
    {
        TeamA,
        TeamB,
        Unselected
    }
}
