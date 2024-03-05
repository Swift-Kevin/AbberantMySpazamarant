using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimerCounter))]
public class WeaponBase : MonoBehaviour
{
    [Serializable]
    protected struct WeaponInfo
    {
        [SerializeField] private float cooldown;
        [SerializeField] private float attackDistance;
        [SerializeField] private string animName;

        public float CD => cooldown;
        public float AtkDist => attackDistance;
        public string AnimationName => animName;
    }

    [SerializeField] protected TimerCounter weaponTimer;
    [SerializeField] protected AttackDamagePool attack;
    [SerializeField] protected WeaponInfo weapon;

    protected bool canUseWeapon;
    public bool CanUse => canUseWeapon;

    private void Start()
    {
        attack.SetToMax();
        canUseWeapon = true;

        if (weaponTimer == null)
        {
            weaponTimer = GetComponent<TimerCounter>();
        }
    }

    private void OnEnable()
    {
        weaponTimer.OnEnded += AttackTimer_OnEnded;
        weaponTimer.OnStarted += AttackTimer_OnStart;
        weaponTimer.OnRestarted += AttackTimer_OnStart;
    }

    private void OnDisable()
    {
        weaponTimer.OnEnded -= AttackTimer_OnEnded;
        weaponTimer.OnStarted -= AttackTimer_OnStart;
        weaponTimer.OnRestarted -= AttackTimer_OnStart;
    }

    private void AttackTimer_OnStart()
    {
        canUseWeapon = false;
    }

    private void AttackTimer_OnEnded()
    {
        weaponTimer.enabled = false;
        canUseWeapon = true;
    }

    public virtual void Attack()
    {
        Debug.Log("Base Version Called");
    }
}
