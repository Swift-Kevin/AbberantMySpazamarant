using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameStates
{
    Playing,
    Paused,
    Died,
    Win,
    MainMenu,
    Other
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameStates currentGameState;

    private float levelNumber = 0;
    private float levelMax = 5;
    private float origTimeScale = 0;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 60;
        origTimeScale = Time.timeScale;
        UIManager.Instance.TurnOnGamePlayingUI();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetupNextLevel()
    {
        levelNumber += 1;
    }

    public void RetryCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void PauseGameStatus()
    {
        if (currentGameState == GameStates.Playing)
        {
            currentGameState = GameStates.Paused;
            UIManager.Instance.DisplayPauseUI();
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            currentGameState = GameStates.Playing;
            UIManager.Instance.TurnOffPauseUI();
            Time.timeScale = origTimeScale;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        PauseGameStatus();
    }

    private void Update()
    {
        if (InputManager.Instance.EscapePressed)
        {
            PauseGameStatus();
        }
    }
}
