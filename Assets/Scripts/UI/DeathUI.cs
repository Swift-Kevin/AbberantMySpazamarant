using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : BaseUI
{
    [SerializeField] private Button retryLevel;
    [SerializeField] private Button quitGameButton;

    private void Start()
    {
        retryLevel.onClick.AddListener(GameManager.Instance.RetryCurrentLevel);
        quitGameButton.onClick.AddListener(GameManager.Instance.QuitGame);
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
