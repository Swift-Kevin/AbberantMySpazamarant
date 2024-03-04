using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase instance;

    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] private PlayerStatManager statManager;

    public PlayerStatManager Stats => statManager;
    
    public Rigidbody rigidBody;



    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (statManager == null)
            statManager = GetComponent<PlayerStatManager>();
        if (movementScript == null)
            movementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        movementScript.Movement();
    }
}
