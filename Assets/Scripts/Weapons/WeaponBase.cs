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
        [SerializeField] private float secondaryAttackDistance;
        [SerializeField] private List<string> animNames;
        [SerializeField] private List<string> secondaryAnimNames;

        public float CD => cooldown;
        public float AtkDist => attackDistance;
        public float SecAtkDist => secondaryAttackDistance;
        public List<string> AnimationNames => animNames;
        public string RandAnimName => animNames[UnityEngine.Random.Range(0, animNames.Count)];
        public string RandSecAnimName => secondaryAnimNames[UnityEngine.Random.Range(0, secondaryAnimNames.Count)];
    }

    [Space]
    [Header("Base Values")]
    [SerializeField] protected TimerCounter weaponTimer;
    [SerializeField] protected AttackDamagePool attack;
    [SerializeField] protected WeaponInfo weapon;

    [Space]
    [Header("Animations for Sheating/Unsheating")]
    [SerializeField] protected string sheatheAnim;
    [SerializeField] protected string unsheatheAnim;

    protected bool canUseWeapon;
    public bool CanUse => canUseWeapon;

    public virtual void Start()
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
        Debug.Log("Base Version [ATTACK] Called");
    }

    public virtual void SpecialAttack()
    {
        Debug.Log("Base Version [SPECIAL] ATTACK Called");
    }

    public virtual void FixBadValues()
    {
        Debug.Log("Base Version [FIXBADVALUES] Called");
    }

    public virtual void Sheathe()
    {
        Debug.Log("Base Version [SHEATHE] Called");
    }

    public virtual void Unsheathe()
    {
        Debug.Log("Base Version [UNSHEATHE] Called");
    }
}
