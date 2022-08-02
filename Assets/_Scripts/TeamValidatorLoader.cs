using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

//this makes sure that each player has picked a different team, and then loads them into the game
public class TeamValidatorLoader : MonoBehaviour
{
    public string LevelToLoadName; //just an inspector assigned string. Could have hardcoded, but I was changing the name of this frequently at the time
    public TextMeshProUGUI LogText; //this shows you if you have done sth wrong

    public Button HostConfirmButton; //references to confirm buttons
    public Button GuestConfirmButton;

    public bool isHostReady = false; //flags to make sure if we can just to the next level
    public bool isGuestReady = false;

    HostTeamPicker hostPicker;
    GuestTeamPicker guestPicker;
    PhotonView view;

    void Start()
    {
        hostPicker = FindObjectOfType<HostTeamPicker>();
        guestPicker = FindObjectOfType<GuestTeamPicker>();

        LogText.text = ""; //empty string at start

        view = GetComponent<PhotonView>();
        //confirmation buttons are hidden if you havent moved from the default state in the middle
        HostConfirmButton.gameObject.SetActive(false);
        GuestConfirmButton.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //show the relevant confirm buttons for the host and the guest
    public void ShowHostConfirmButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            HostConfirmButton.gameObject.SetActive(true);
        }
        else
        {
            HostConfirmButton.gameObject.SetActive(false);
        }
    }
    public void ShowGuestConfirmButton()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            GuestConfirmButton.gameObject.SetActive(true);
        }
        else
        {
            GuestConfirmButton.gameObject.SetActive(false);
        }
    }

    //call rpc which makes host ready. has to be rpc for both players to access this info
    public void MakeHostReady()
    {
        view.RPC("MakeHostReadyRPC", RpcTarget.All);
    }

    [PunRPC]
    void MakeHostReadyRPC()
    {
        if (ConfirmFirstPlayer())
        {
            isHostReady = true;
            HostConfirmButton.gameObject.SetActive(false);

            if (PhotonNetwork.IsMasterClient)
            {
                hostPicker.DisableButtonsAfterConfirmation(); //after i confirm, i disable the team chocie buttons
                LogText.text = "You are ready! Waiting for the other player...";
            }
        }

    }
    //same thing for the guest
    public void MakeGuestReady()
    {
        view.RPC("MakeGuestReadyRPC", RpcTarget.All);
    }

    [PunRPC]
    void MakeGuestReadyRPC()
    {
        if (ConfirmFirstPlayer())
        {
            isGuestReady = true;
            GuestConfirmButton.gameObject.SetActive(false);

            if (!PhotonNetwork.IsMasterClient)
            {
                guestPicker.DisableButtonsAfterConfirmation();
                LogText.text = "You are ready! Waiting for the other player...";
            }
        }

    }

    //here i validate if both players have picked a different team
    public bool ValidateTeams()
    {
        GameController gm = GameController.Instance;
        //if it isnt the same team
        if (gm.Player1Choice.Team != gm.Player2Choice.Team)
        {
            //and no player is unselected
            if (gm.Player1Choice.Team != PlayerSelection.TeamSelection.Unselected &&
                gm.Player2Choice.Team != PlayerSelection.TeamSelection.Unselected)
            {
                Debug.Log($"Player1 Team: {gm.Player1Choice.Team}; Player 2 Team: " +
                    $"{gm.Player2Choice.Team}");
                return true; //then its different teams, returns true
            }
        }
        Debug.Log($"Player1 Team: {gm.Player1Choice.Team}; Player 2 Team: " +
                    $"{gm.Player2Choice.Team}");
        //otherwise either same team, or unselected
        return false;
    }

    //when you press the confirm button, you validate as well
    public bool ConfirmFirstPlayer()
    {
        if (isHostReady == false && isGuestReady == false)
        {
            return true;
        }
        else
        {
            if (ValidateTeams() == false)
            {
                Debug.Log("Players must be in different teams!");
                LogText.text = "You cannot be in the same team!";
                return false;
            }

            if (ValidateTeams())
            {
                //here we can load
                Debug.Log("Loading could commence!");
                LoadGame();

            }
            return true;
        }
    }

    //load the game level as RPC for both players, once both have confirmed
    public void LoadGame()
    {
        view.RPC("LoadGameRPC", RpcTarget.All);
    }

    [PunRPC]
    void LoadGameRPC()
    {
        LogText.text = "Load the game!";
        PhotonNetwork.LoadLevel(LevelToLoadName);
    }
}
