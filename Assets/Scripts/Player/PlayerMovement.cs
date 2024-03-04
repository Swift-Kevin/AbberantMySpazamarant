using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] private float sprintModifier = 20f;
    private Rigidbody rigidBody;

    void Start()
    {
        if (rigidBody == null)
            rigidBody = PlayerBase.instance.rigidBody;
    }

    public void Movement()
    {
        Vector2 move = InputManager.Instance.MoveVect * PlayerBase.instance.Stats.Speed.CurrValue;
        rigidBody.velocity = transform.TransformDirection(new Vector3(move.x, rigidBody.velocity.y, move.y));
    }
}
