using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    public Slider Player1ScoreSlider;
    public Slider Player2ScoreSlider;

    private int p1Score = 0;
    private int p2Score = 0;

    private GameOver gameOver;
    private PointScoredHandler pointScoredHandler;

    private void Start()
    {
        gameOver = FindObjectOfType<GameOver>();
        pointScoredHandler = FindObjectOfType<PointScoredHandler>();
    }
    public void UpdateP1Score()
    {
        p1Score++;
        GameController.Instance.Player1Choice.PlayerPoints = p1Score;
        Player1ScoreSlider.value = (int)p1Score;
        if (p1Score == 3 || GameController.Instance.Player1Choice.PlayerPoints == 3)
        {
            gameOver.ShowGameOverScreen();
        }
        pointScoredHandler.DestroyRelevantObjects();
    }

    public void UpdateP2Score()
    {
        p2Score++;
        GameController.Instance.Player2Choice.PlayerPoints = p2Score;
        Player2ScoreSlider.value = (int)p2Score;

        if (p2Score == 3 || GameController.Instance.Player2Choice.PlayerPoints == 3)
        {
            gameOver.ShowGameOverScreen();
        }
        pointScoredHandler.DestroyRelevantObjects();
    }
}
