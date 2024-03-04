using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Resource
{
    // Events for Resource to use
    public event System.Action OnDepleted;
    public event System.Action OnFilled;
    public event System.Action OnDecrease;
    public event System.Action OnIncrease;

    // Set Values
    [SerializeField] private float maxValue = 0;
    [SerializeField] private float minValue = 0;

    // Resource States
    [SerializeField]
    private float currentValue;
    private bool depletedResource;

    // Properties
    public float CurrValue => currentValue;
    public float Max => maxValue;
    public float Min => minValue;
    public bool Valid => (currentValue > 0);

    public bool UpdateMaxValue(float amount)
    {
        bool canUse = minValue < (maxValue + amount);
        if (canUse)
        {
            maxValue += amount;
        }
        return canUse;
    }

    public void SetToMax()
    {
        currentValue = maxValue;
        OnFilled?.Invoke();
        depletedResource = false;
    }

    public void SetToMin()
    {
        currentValue = minValue;
        OnDepleted?.Invoke();
        depletedResource = true;
    }

    public void Increase(float amount)
    {
        currentValue += Mathf.Min(currentValue + amount, maxValue);
        OnIncrease?.Invoke();
        depletedResource = false;
    }

    public void Decrease(float amount)
    {
        if (depletedResource)
            return;

        currentValue = Mathf.Max(currentValue - amount, 0);
        OnDecrease?.Invoke();
        if (currentValue == 0)
        {
            OnDepleted?.Invoke();
            depletedResource = true;
        }
    }

    public bool UseResource(float amount)
    {
        bool canUse = currentValue >= amount;
        if (canUse)
        {
            Decrease(amount);
        }
        return canUse;
    }

}
