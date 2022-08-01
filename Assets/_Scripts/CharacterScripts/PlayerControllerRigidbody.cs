using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerControllerRigidbody : MonoBehaviour
{
    Vector3 playerMovementInput;
    PhotonView view;

    [SerializeField]
    private Rigidbody PlayerBody;

    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
    }
    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            movePlayer();
        }

    }

    private void movePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMovementInput) * Speed;
        PlayerBody.velocity = new Vector3(moveVector.x, PlayerBody.velocity.y, moveVector.z);
    }
}
