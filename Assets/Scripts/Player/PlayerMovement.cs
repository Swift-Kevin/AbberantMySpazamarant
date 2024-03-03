using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerStatManager statManager;
    [SerializeField] private Rigidbody rigidBody;

    void Start()
    {
        if (statManager == null)
            statManager = GetComponent<PlayerStatManager>();
        if (rigidBody == null)
            rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 move = InputManager.Instance.MoveVect * statManager.Speed.CurrValue;
        rigidBody.velocity = transform.TransformDirection(new Vector3(move.x, rigidBody.velocity.y, move.y));
    }
}
