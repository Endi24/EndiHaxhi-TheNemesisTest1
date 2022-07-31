using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerSelection PlayerChoice;
}

public class PlayerSelection
{
    public TeamSelection team;
    public string CharacterName;

    public enum TeamSelection
    {
        TeamA,
        TeamB
    }
}
