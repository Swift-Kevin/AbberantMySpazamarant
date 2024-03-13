using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour, IDamageable
{
    public static PlayerBase Instance;

    [SerializeField] private PlayerMovement movementScript;
    [SerializeField] private PlayerStatManager statManager;
    [SerializeField] private Camera cam;
    [SerializeField] private WeaponManager weaponsManager;
    public float WeaponCooldownTimer => weaponsManager.WeaponCooldownTimer;

    public PlayerStatManager Stats => statManager;
    public float SpeedVal => Stats.Speed.CurrValue;
    public event Action OnValueUpdated;


    public Vector3 CamFWD => cam.transform.forward;

    public void TakeDamage(float damage)
    {
        statManager.TakeDMG(damage);
        OnValueUpdated?.Invoke();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (statManager == null)
        {
            statManager = GetComponent<PlayerStatManager>();
        }

        if (movementScript == null)
        {
            movementScript = GetComponent<PlayerMovement>();
        }

        UIManager.Instance.UpdatePlayerUI();
    }

    // Update is called once per frame
    void Update()
    {
        movementScript.Movement();
    }
}
