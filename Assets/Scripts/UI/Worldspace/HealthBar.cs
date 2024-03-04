using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private EnemyStatManager stats;

    // Start is called before the first frame update
    void Start()
    {
       Health_OnChanged();
    }

    private void OnEnable()
    {
        stats.Health.OnChanged += Health_OnChanged;
    }

    private void OnDisable()
    {
        stats.Health.OnChanged -= Health_OnChanged;
    }

    private void Health_OnChanged()
    {
        fill.fillAmount = stats.Health.Percent;
    }
}
