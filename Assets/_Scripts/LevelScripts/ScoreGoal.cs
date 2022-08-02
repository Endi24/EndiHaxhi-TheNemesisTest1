using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGoal : MonoBehaviour
{
    //the goal for the specific player, inserted in the inspector
    public string PlayerString;
    private ScoreUpdater scoreUpdater; //referenco to score updator, which will handle the updating

    private void Start()
    {
        scoreUpdater = FindObjectOfType<ScoreUpdater>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if the ball enters, check if it was a score for player1/host or player2/guest
        if (other.CompareTag("Ball"))
        {
            Debug.Log($"{PlayerString} scored at goal: {this.gameObject.name}");
            if (PlayerString == "Player1")
            {
                scoreUpdater.UpdateP1Score();
            }
            else if (PlayerString == "Player2")
            {
                scoreUpdater.UpdateP2Score();
            }
        }
    }
}
