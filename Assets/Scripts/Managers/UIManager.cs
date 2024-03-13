using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Scripts")]
    [SerializeField] private PlayerUI playerUIScript;
    [SerializeField] private ScoreUI scoreUIScript;
    [SerializeField] private PauseUI pauseUIScript;
    [SerializeField] private WinUI winUIScript;
    [SerializeField] private DeathUI deathUIScript;
    [SerializeField] private MainMenuUI mainMenuUIScript;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        TurnOffAllUI();
        TurnOnGamePlayingUI();

        PlayerBase.Instance.OnValueUpdated += UpdatePlayerUI;
    }

    private void OnDisable()
    {
        PlayerBase.Instance.OnValueUpdated -= UpdatePlayerUI;
    }

    private void TurnOffAllUI()
    {
        playerUIScript.TurnOffObject();
        scoreUIScript.TurnOffObject();
        pauseUIScript.TurnOffObject();
        deathUIScript.TurnOffObject();
        winUIScript.TurnOffObject();
        mainMenuUIScript.TurnOffObject();
    }

    public void TurnOnGamePlayingUI()
    {
        DisplayPlayerUI();
        DisplayScoreUI();

        UpdatePlayerUI();
    }

    // UPDATE UI 

    public void UpdatePlayerUI()
    {
        playerUIScript.UpdateUI();
    }

    public void UpdateScoreUI()
    {
        scoreUIScript.UpdateUI();
    }
    
    // DISPLAY FUNCTIONS

    public void DisplayWin()
    {
        winUIScript.TurnOnObject();
    }

    public void DisplayLose()
    {
        deathUIScript.TurnOnObject();
    }

    public void DisplayScoreUI()
    {
        scoreUIScript.TurnOnObject();
    }

    public void TurnOffDisplayScore()
    {
        scoreUIScript.TurnOffObject();
    }

    public void DisplayPauseUI()
    {
        pauseUIScript.TurnOnObject();
    }

    public void TurnOffPauseUI()
    {
        pauseUIScript.TurnOffObject();
    }

    public void DisplayPlayerUI()
    {
        playerUIScript.TurnOnObject();
    }

    public void TurnOffPlayerUI()
    {
        playerUIScript.TurnOffObject();
    }
}
