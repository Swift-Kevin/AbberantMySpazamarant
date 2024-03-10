using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float gravityScale;
    private Vector3 move;
    private Vector3 playerVelocity;

    public void Movement()
    {
        move = (transform.forward * InputManager.Instance.MoveVect.y) + (transform.right * InputManager.Instance.MoveVect.x);
        controller.Move(move * Time.deltaTime * PlayerBase.Instance.SpeedVal);
        playerVelocity.y -= gravityScale * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
