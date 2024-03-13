using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : BaseUI
{
    [SerializeField] private Image playerHealthSlider;
    [SerializeField] private Image swapWeaponCooldown;

    public override void UpdateUI()
    {
        playerHealthSlider.fillAmount = PlayerBase.Instance.Stats.Health.Percent;
        swapWeaponCooldown.fillAmount = PlayerBase.Instance.WeaponCooldownTimer;
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
