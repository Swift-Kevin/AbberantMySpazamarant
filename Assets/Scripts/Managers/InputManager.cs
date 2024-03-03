using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    PlayerInput input;


    public PlayerInput.GeneralActions Action => input.General;
    public Vector2 MoveVect => input.General.Move.ReadValue<Vector2>();

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        input = new PlayerInput();
        input.General.Enable();
    }

    public Vector2 CameraReadVal()
    {
        return input.General.Look.ReadValue<Vector2>();
    }
}
