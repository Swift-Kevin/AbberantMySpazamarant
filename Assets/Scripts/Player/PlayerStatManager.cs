using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    [SerializeField] private HealthPool health;
    [SerializeField] private SpeedPool speed;
    public HealthPool Health => health;
    public SpeedPool Speed => speed;
    
    // Start is called before the first frame update
    void Start()
    {
        SetBaseValues();
    }

    private void SetBaseValues()
    {
        health.SetToMax();
        speed.SetToMax();
    }

    public void TakeDMG(float value)
    {
        health.Decrease(value);
    }
}
