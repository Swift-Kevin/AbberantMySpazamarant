using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimerCounter))]
public class WeaponBase : MonoBehaviour
{
    [SerializeField] private AttackDamagePool attack;
    [SerializeField] private TimerCounter attackTimer;
    [SerializeField] private float attackDist;
    [SerializeField] private float cooldown = 3f;

    private bool canUseWeapon;
    public bool CanUse => canUseWeapon;

    void Start()
    {
        attack.SetToMax();
        canUseWeapon = true;

        if (attackTimer == null)
        {
            attackTimer = GetComponent<TimerCounter>();
        }
    }

    private void OnEnable()
    {
        attackTimer.OnEnded += AttackTimer_OnEnded;
        attackTimer.OnStarted += AttackTimer_OnStart;
        attackTimer.OnRestarted += AttackTimer_OnStart;
    }

    private void OnDisable()
    {
        attackTimer.OnEnded -= AttackTimer_OnEnded;
        attackTimer.OnStarted -= AttackTimer_OnStart;
        attackTimer.OnRestarted -= AttackTimer_OnStart;
    }

    private void AttackTimer_OnStart()
    {
        canUseWeapon = false;
        Debug.Log("Timer started/restarted");
    }

    private void AttackTimer_OnEnded()
    {
        attackTimer.enabled = false;
        canUseWeapon = true;
    }

    void Update()
    {
        if (InputManager.Instance.Action.Attack.WasPressedThisFrame() && canUseWeapon)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, attackDist))
            {
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.TakeDamage(attack.CurrValue);
                    attackTimer.enabled = true;
                    attackTimer.StartTimer(cooldown);
                }
            }
        }
    }
}
