using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class TeamValidatorLoader : MonoBehaviour
{
    public TextMeshProUGUI LogText;

    public Button HostConfirmButton;
    public Button GuestConfirmButton;

    public bool isHostReady = false;
    public bool isGuestReady = false;

    HostTeamPicker hostPicker;
    GuestTeamPicker guestPicker;
    PhotonView view;

    void Start()
    {
        hostPicker = FindObjectOfType<HostTeamPicker>();
        guestPicker = FindObjectOfType<GuestTeamPicker>();

        LogText.text = "";

        view = GetComponent<PhotonView>();
        HostConfirmButton.gameObject.SetActive(false);
        GuestConfirmButton.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

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
                hostPicker.DisableButtonsAfterConfirmation();
                LogText.text = "You are ready! Waiting for the other player...";
            }
        }

    }

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

    public bool ValidateTeams()
    {
        GameController gm = GameController.Instance;
        if (gm.Player1Choice.Team != gm.Player2Choice.Team)
        {
            if (gm.Player1Choice.Team != PlayerSelection.TeamSelection.Unselected &&
                gm.Player2Choice.Team != PlayerSelection.TeamSelection.Unselected)
            {
                Debug.Log($"Player1 Team: {gm.Player1Choice.Team}; Player 2 Team: " +
                    $"{gm.Player2Choice.Team}");
                return true;
            }
        }
        Debug.Log($"Player1 Team: {gm.Player1Choice.Team}; Player 2 Team: " +
                    $"{gm.Player2Choice.Team}");
        return false;
    }

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

    public void LoadGame()
    {
        view.RPC("LoadGameRPC", RpcTarget.All);
    }

    [PunRPC]
    void LoadGameRPC()
    {
        LogText.text = "Load the game!";
        PhotonNetwork.LoadLevel("InGame2D");
    }
}
