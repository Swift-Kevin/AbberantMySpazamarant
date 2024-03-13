using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI text_CurrentKills;
    [SerializeField] private TextMeshProUGUI text_CurrentTotal;

    public override void UpdateUI()
    {
        text_CurrentKills.text = EnemyManager.Instance.TotalKills.ToString();
        text_CurrentTotal.text = EnemyManager.Instance.TotalEnemies.ToString();
    }

    public override void TurnOffObject()
    {
        UpdateUI();
        associatedGameObj.SetActive(false);
    }

    public override void TurnOnObject()
    {
        UpdateUI();
        associatedGameObj.SetActive(true);
    }
}
