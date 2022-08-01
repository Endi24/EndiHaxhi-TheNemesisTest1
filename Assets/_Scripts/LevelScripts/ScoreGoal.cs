using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGoal : MonoBehaviour
{
    public string PlayerString;
    private ScoreUpdater scoreUpdater;

    private void Start()
    {
        scoreUpdater = FindObjectOfType<ScoreUpdater>();
    }
    private void OnTriggerEnter(Collider other)
    {
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
