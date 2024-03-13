using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(TimerCounter))]
public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponBase currentWeapon;
    [Space]
    [SerializeField] private DaggerRegular dagger1;
    [SerializeField] private DaggerAbberant dagger2;
    [Space]
    [SerializeField] private GameObject dagger1Model;
    [SerializeField] private GameObject dagger2Model;

    [Space]
    [SerializeField] private TimerCounter timerSwapWeapons;
    [SerializeField] private TimerCounter timerSheathingAndUnsheating;
    public float WeaponCooldownTimer => timerSwapWeapons.Percent;

    private bool canSwap;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = dagger1;
        canSwap = true;
        UIManager.Instance.UpdatePlayerUI();

        dagger1Model.SetActive(true);
        dagger2Model.SetActive(false);
        timerSwapWeapons = GetComponent<TimerCounter>();

        timerSwapWeapons.OnStarted += TimerSwapWeapons_OnStarted;
        timerSwapWeapons.OnTick += TimerSwapWeapons_OnTick;
        timerSwapWeapons.OnEnded += TimerSwapWeapons_OnEnded;

        timerSheathingAndUnsheating.OnEnded += FinishSwap;

        if (InputManager.Instance != null)
        {
            InputManager.Instance.Action.SpecialAttack.started += SpAtk;
            InputManager.Instance.Action.Attack.started += Atk;
            InputManager.Instance.Action.SwapWeapon.started += SwapWeapon;
        }
    }

    private void TimerSwapWeapons_OnTick()
    {
        UIManager.Instance.UpdatePlayerUI();
    }

    private void OnDisable()
    {
        timerSwapWeapons.OnStarted -= TimerSwapWeapons_OnStarted;
        timerSwapWeapons.OnTick -= TimerSwapWeapons_OnTick;
        timerSwapWeapons.OnEnded -= TimerSwapWeapons_OnEnded;

        timerSheathingAndUnsheating.OnEnded -= FinishSwap;

        InputManager.Instance.Action.SpecialAttack.started -= SpAtk;
        InputManager.Instance.Action.Attack.started -= Atk;
        InputManager.Instance.Action.SwapWeapon.started -= SwapWeapon;

    }

    private void TimerSwapWeapons_OnEnded()
    {
        canSwap = true;
    }

    private void TimerSwapWeapons_OnStarted()
    {
        canSwap = false;
    }

    private void Atk(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        currentWeapon.Attack();
    }

    private void SpAtk(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        currentWeapon.SpecialAttack();
    }

    private void SwapWeapon(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (canSwap)
        {
            // Fix current weapon (PRE SWAP)
            currentWeapon.FixBadValues();
            currentWeapon.Sheathe();
            timerSheathingAndUnsheating.StartTimer();
        }
    }

    private void FinishSwap()
    {
        if (currentWeapon == dagger1)
        {
            currentWeapon = dagger2;
            dagger1Model.SetActive(false);
            dagger2Model.SetActive(true);
        }
        else
        {
            currentWeapon = dagger1;
            dagger1Model.SetActive(true);
            dagger2Model.SetActive(false);
        }

        // Fix current weapon (POST SWAP)
        currentWeapon.FixBadValues();
        timerSwapWeapons.StartTimer();
        currentWeapon.Unsheathe();
    }

}
