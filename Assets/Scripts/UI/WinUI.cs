using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : BaseUI
{
    [SerializeField] private Button nextLevel;
    [SerializeField] private Button quitGameButton;

    private void Start()
    {
        nextLevel.onClick.AddListener(GameManager.Instance.SetupNextLevel);
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
