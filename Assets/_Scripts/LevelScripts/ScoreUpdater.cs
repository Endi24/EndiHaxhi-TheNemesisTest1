using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    public Slider Player1ScoreSlider; //visual representation of the score for host
    public Slider Player2ScoreSlider; //visual representation of the score for guest

    public GameObject PlayerScoredPanel; //ui that shows when player scores
    public TextMeshProUGUI PlayerScoredText; //text for the same thing

    private GameOver gameOver;
    private PhotonView view;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        Player1ScoreSlider.value = GameController.Instance.Player1Choice.PlayerPoints; //update the sliders with the score from GameController
        Player2ScoreSlider.value = GameController.Instance.Player2Choice.PlayerPoints;
        gameOver = FindObjectOfType<GameOver>();
    }
    public void UpdateP1Score()
    {
        //update the score for p1/host in the Game Controller
        GameController.Instance.Player1Choice.PlayerPoints++; 
        Player1ScoreSlider.value = (int)GameController.Instance.Player1Choice.PlayerPoints; //update the slider

        //check if 3 have been scored, then call show game over screen
        if (GameController.Instance.Player1Choice.PlayerPoints == 3)
        {
            gameOver.ShowGameOverScreen();
        }
        else
        {
            pointScored(true); //else, just update the score
        }
    }

    //same thing as above but for guest
    public void UpdateP2Score()
    {
        //p2Score++;
        GameController.Instance.Player2Choice.PlayerPoints++; // = p2Score;
        Player2ScoreSlider.value = (int)GameController.Instance.Player2Choice.PlayerPoints;

        if (GameController.Instance.Player2Choice.PlayerPoints == 3)
        {
            gameOver.ShowGameOverScreen();
        }
        else
        {
            pointScored(false);
        }
    }

    //if a point was scored 
    private void pointScored(bool wasHost)
    {
        //activate the score panel
        PlayerScoredPanel.SetActive(true); 
        if (wasHost) //was it host?
        {
            //update the text for each player
            if (PhotonNetwork.IsMasterClient)
            {                
                PlayerScoredText.text = "You Scored! Yay!";
            }
            else if (!PhotonNetwork.IsMasterClient)
            {
                PlayerScoredText.text = "Enemy Scored! Defend Better!";
            }
        }
        else if (!wasHost) //if it was guest
        {
            //update the text as needed
            if (!PhotonNetwork.IsMasterClient)
            {
                PlayerScoredText.text = "You Scored! Yay!";
            }
            else if (PhotonNetwork.IsMasterClient)
            {
                PlayerScoredText.text = "Enemy Scored! Defend Better!";
            }
        }

        //we only need to call load game again on the master client/host
        if (PhotonNetwork.IsMasterClient)
        {
            Invoke("LoadGameAgain", 1.5f); //loads the same level with a 1.5second delay
        }
    }
    void LoadGameAgain()
    {
        //calls the rpc function for both players
        view.RPC("LoadGameAgainRPC", RpcTarget.All);
    }

    [PunRPC]
    void LoadGameAgainRPC()
    {
        PhotonNetwork.LoadLevel("InGame3D"); //simply reloads the level
    }
}
