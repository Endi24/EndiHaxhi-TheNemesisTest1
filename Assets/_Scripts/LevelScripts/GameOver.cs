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
    //an enum which makes it a bit clearer (semantically) who is the winning player
    public enum WinningPlayer
    {
        Player1Host,
        Player2Guest
    }
    private PhotonView view;
    private WinningPlayer winningPlayer;

    public GameObject GameOverUI; //ui that shows when game is over
    public TextMeshProUGUI EndGameMessage; //the message when game is over

    public GameObject DisconnectUI; //same thing but for disconnect
    public TextMeshProUGUI DisconnectMessage; 

    public bool AnyoneDisconnected; //unused logic, I found a better way of handling if anyone left

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }
    public void ShowGameOverScreen()
    {
        //we have to use RPCs to show the screen for both players
        view.RPC("ShowGameOverScreenRPC", RpcTarget.All);
    }

    //has to be rpc
    [PunRPC]
    void ShowGameOverScreenRPC()
    {
        //determine who won
        DetermineWinnerScore();
        GameOverUI.SetActive(true); //activate the ui

        //if the host won
        if (winningPlayer == WinningPlayer.Player1Host)
        {
            //then that is the master client, so show them that they won
            if (PhotonNetwork.IsMasterClient)
            {
                EndGameMessage.text = "You Won!!!";
            }
            //and if it isnt the master client, it means its the guest, so show them that they lost
            else if (!PhotonNetwork.IsMasterClient)
            {
                EndGameMessage.text = "You lost :(";
            }
            
        }
        //we do the opposite here, if the guest won
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
        //after a victory, we want both players to go to team select
        redirectToTeamSelect();
    }

    //determines who won by reaching 3 points
    public void DetermineWinnerScore()
    {
        //we need to update the values in the game controller, so we have access when we reload the scene
        int p1Score = GameController.Instance.Player1Choice.PlayerPoints;
        int p2Score = GameController.Instance.Player2Choice.PlayerPoints;

        //if anyone reached 3 points
        if (p1Score == 3 || p2Score == 3)
        {
            Debug.Log("Any has a score of 3!");

            //see who won and make them the winning player
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
    //the player can leave at any time
    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectMe()); //start the disconnect coroutine
    }
    //removes the player from the room
    IEnumerator DisconnectMe()
    {
        PhotonNetwork.LeaveRoom();
        while(PhotonNetwork.InRoom)
            yield return null;

        //reset the score too in GameController, so if someone left, the score is always reset
        GameController.Instance.Player1Choice.PlayerPoints = 0;
        GameController.Instance.Player2Choice.PlayerPoints = 0;

        //precaution, but this ensures that the custom properties are reset. in this case, the team image
        PhotonNetwork.LocalPlayer.CustomProperties.Remove("TeamImage1");
        PhotonNetwork.LocalPlayer.CustomProperties.Remove("TeamImage2");
        SceneManager.LoadScene("ConnectToMaster"); //since only the local player is leaving, then no need for photonnetwork.loadlevel
    }

    //callback that happens when a player leaves
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"Player {otherPlayer.NickName} disconnected");
        DisconnectUI.SetActive(true); //activate the disconnect ui and text
        DisconnectMessage.text = $"{otherPlayer.NickName} disconnected, so You WIN!";
    }

    //testing function, not used anymore
    public void GoToMatchMakingButton()
    {
        SceneManager.LoadScene("ConnectToMaster");
    }
    //when a player won, then redirect both to team select after 1.5seconds
    void redirectToTeamSelect()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Invoke("redirectGame", 1.5f);
        }
    }
    //has to be rpc to redirect both
    void redirectGame()
    {
        view.RPC("redirectGameRPC", RpcTarget.All);
    }

    [PunRPC]
    void redirectGameRPC()
    {
        //removes the custom properties
        PhotonNetwork.LocalPlayer.CustomProperties.Remove("TeamImage1");
        PhotonNetwork.LocalPlayer.CustomProperties.Remove("TeamImage2");

        //reset the score too in GameController
        GameController.Instance.Player1Choice.PlayerPoints = 0;
        GameController.Instance.Player2Choice.PlayerPoints = 0;
        //loads for both the team select screen
        PhotonNetwork.LoadLevel("TeamSelect");
    }

}
