using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : BaseUI
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        continueButton.onClick.AddListener(GameManager.Instance.PauseGameStatus);
        quitButton.onClick.AddListener(GameManager.Instance.QuitGame);
    }

    public override void TurnOffObject()
    {
        associatedGameObj.SetActive(false);
    }

    public override void TurnOnObject()
    {
        associatedGameObj.SetActive(true);
    }
}
