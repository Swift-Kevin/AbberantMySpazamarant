using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    PlayerInput input;

    public PlayerInput.GeneralActions Action => input.General;
    public Vector2 MoveVect => Action.Move.ReadValue<Vector2>();

    public bool SwapWeaponPressed => input.General.SwapWeapon.WasPressedThisFrame();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        input = new PlayerInput();
        input.Enable();
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
