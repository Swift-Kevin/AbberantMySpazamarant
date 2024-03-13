using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    PlayerInput input;

    public PlayerInput.GeneralActions Action => input.General;
    public Vector2 MoveVect => Action.Move.ReadValue<Vector2>();

    public bool SwapWeaponPressed => input.General.SwapWeapon.WasPressedThisFrame();
    public bool SpecialAttackPressed => input.General.SpecialAttack.WasPressedThisFrame();
    public bool EscapePressed => Action.Paused.WasPressedThisFrame();


    private void Awake()
    {
        input = new PlayerInput();
        input.Enable();
        Instance = this;
    }

    void Start()
    {
    }

    public Vector2 CameraReadVal()
    {
        return input.General.Look.ReadValue<Vector2>();
    }

    public bool CheckSprint()
    {
        return Action.Sprint.ReadValue<bool>();
    }
}
