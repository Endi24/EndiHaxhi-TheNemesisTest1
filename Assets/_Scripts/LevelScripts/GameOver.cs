using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviourPunCallbacks
{
    public enum WinningPlayer
    {
        Player1Host,
        Player2Guest
    }
    private PhotonView view;
    private WinningPlayer winningPlayer;

    public GameObject GameOverUI;
    public TextMeshProUGUI EndGameMessage;

    public GameObject DisconnectUI;
    public TextMeshProUGUI DisconnectMessage;

    public bool AnyoneDisconnected;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    public void ShowGameOverScreen()
    {
        view.RPC("ShowGameOverScreenRPC", RpcTarget.All);
    }

    //has to be rpc
    [PunRPC]
    void ShowGameOverScreenRPC()
    {
        DetermineWinnerScore();
        GameOverUI.SetActive(true);

        if (winningPlayer == WinningPlayer.Player1Host)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                EndGameMessage.text = "You Won!!!";
            }
            else if (!PhotonNetwork.IsMasterClient)
            {
                EndGameMessage.text = "You lost :(";
            }
            
        }
        else if (winningPlayer == WinningPlayer.Player2Guest)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                EndGameMessage.text = "You lost :(";
            }
            else if (!PhotonNetwork.IsMasterClient)
            {
                EndGameMessage.text = "You Win!!!";
            }
        }

        redirectToTeamSelect();
    }

    public void DetermineWinnerScore()
    {

        int p1Score = GameController.Instance.Player1Choice.PlayerPoints;
        int p2Score = GameController.Instance.Player2Choice.PlayerPoints;

        if (p1Score == 3 || p2Score == 3)
        {
            Debug.Log("Any has a score of 3!");

            if (p1Score > p2Score)
            {
                winningPlayer = WinningPlayer.Player1Host;
            }
            else if(p1Score < p2Score)
            {
                winningPlayer = WinningPlayer.Player2Guest;
            }
        }
        else
        {
            Debug.Log("Nobody has a score of 3. Possible disconnect?");
        }

        
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectMe());
    }

    IEnumerator DisconnectMe()
    {
        PhotonNetwork.LeaveRoom();
        while(PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene("ConnectToMaster");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Player {otherPlayer.NickName} disconnected");
        DisconnectUI.SetActive(true);
        DisconnectMessage.text = $"{otherPlayer.NickName} disconnected, so You WIN!";
    }

    public void GoToMatchMakingButton()
    {
        SceneManager.LoadScene("ConnectToMaster");
    }

    void redirectToTeamSelect()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Invoke("redirectGame", 1.5f);
        }
    }

    void redirectGame()
    {
        view.RPC("redirectGameRPC", RpcTarget.All);
    }

    [PunRPC]
    void redirectGameRPC()
    {
        //reset the score too in GameController
        GameController.Instance.Player1Choice.PlayerPoints = 0;
        GameController.Instance.Player2Choice.PlayerPoints = 0;

        PhotonNetwork.LoadLevel("TeamSelect");
    }

}
