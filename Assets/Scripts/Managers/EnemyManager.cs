using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private float enemyTotal;
    [SerializeField] private float enemyKills;

    public float TotalEnemies => enemyTotal;
    public float TotalKills => enemyKills;
    public bool AllEnemiesKilled => TotalEnemies == TotalKills;

    public void AdjustEnemyToTotal(float value)
    {
        UIManager.Instance.UpdateScoreUI();
        enemyTotal += value;
    }

    public void AdjustEnemyKills(float value)
    {
        UIManager.Instance.UpdateScoreUI();
        enemyKills += value;
        
    }
}
