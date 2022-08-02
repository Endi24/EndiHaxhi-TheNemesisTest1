using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    public Slider Player1ScoreSlider;
    public Slider Player2ScoreSlider;

    public GameObject PlayerScoredPanel;
    public TextMeshProUGUI PlayerScoredText;

    private GameOver gameOver;
    private PhotonView view;
    private void Start()
    {
        view = GetComponent<PhotonView>();
        Player1ScoreSlider.value = GameController.Instance.Player1Choice.PlayerPoints;
        Player2ScoreSlider.value = GameController.Instance.Player2Choice.PlayerPoints;
        gameOver = FindObjectOfType<GameOver>();
    }
    public void UpdateP1Score()
    {
        //p1Score++;
        GameController.Instance.Player1Choice.PlayerPoints++; // = p1Score;
        Player1ScoreSlider.value = (int)GameController.Instance.Player1Choice.PlayerPoints;
        if (GameController.Instance.Player1Choice.PlayerPoints == 3)
        {
            gameOver.ShowGameOverScreen();
        }
        else
        {
            pointScored(true);
        }
    }

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

    private void pointScored(bool wasHost)
    {
        PlayerScoredPanel.SetActive(true);
        if (wasHost)
        {
            if (PhotonNetwork.IsMasterClient)
            {                
                PlayerScoredText.text = "You Scored! Yay!";
            }
            else if (!PhotonNetwork.IsMasterClient)
            {
                PlayerScoredText.text = "Enemy Scored! Defend Better!";
            }
        }
        else if (!wasHost)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                PlayerScoredText.text = "You Scored! Yay!";
            }
            else if (PhotonNetwork.IsMasterClient)
            {
                PlayerScoredText.text = "Enemy Scored! Defend Better!";
            }
        }

        if (PhotonNetwork.IsMasterClient)
        {
            Invoke("LoadGameAgain", 1.5f);
        }
    }
    void LoadGameAgain()
    {
        view.RPC("LoadGameAgainRPC", RpcTarget.All);
    }

    [PunRPC]
    void LoadGameAgainRPC()
    {
        PhotonNetwork.LoadLevel("InGame3D");
    }
}
