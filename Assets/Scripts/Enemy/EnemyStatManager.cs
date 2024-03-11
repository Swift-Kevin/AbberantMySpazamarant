using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStatManager : MonoBehaviour
{
    [SerializeField] private HealthPool health;
    [SerializeField] private SpeedPool speed;
    public HealthPool Health => health;
    public SpeedPool Speed => speed;

    // Start is called before the first frame update
    void Start()
    {
        Revive();
    }

    private void SetBaseValues()
    {
        health.SetToMax();
        speed.SetToMax();
    }

    public void Revive()
    {
        SetBaseValues();
    }
}
