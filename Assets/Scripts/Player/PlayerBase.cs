using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase Instance;

    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] private PlayerStatManager statManager;
    [SerializeField] private Camera cam;

    public PlayerStatManager Stats => statManager;
    public float SpeedVal => Stats.Speed.CurrValue;
    

    public Vector3 CamFWD => cam.transform.forward;

    private void Awake()
    {
        Instance = this;
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
