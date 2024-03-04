using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimerCounter : MonoBehaviour
{
    // Events for Resource to use
    public event System.Action OnEnded;
    public event System.Action OnStarted;
    public event System.Action OnRestarted;
    public event System.Action OnTick;

    [SerializeField] private float duration;
    [SerializeField, Tooltip("False = Countdown | True = Stopwatch")] private bool timerDirection; // 0 = down | 1 = up
    [SerializeField] private bool loop;

    private float currentTime;
    public bool runTimer = false;

    private void Update()
    {
        if (runTimer) 
        {
            // decreasing timer (standard countdown)
            if (!timerDirection)
            {
                if (currentTime > 0)
                {
                    // run the timer downwards
                    currentTime -= Time.deltaTime;
                    OnTick?.Invoke();
                }
                else
                {
                    // timer is done
                    TimerEnded();
                }
            }
            // increasing timer (stopwatch effect)
            else
            {
                if (currentTime < duration)
                {
                    // run the timer upwards (counting the stopwatch up)
                    currentTime += Time.deltaTime;
                    OnTick?.Invoke();
                }
                else
                {
                    // timer is finished (we caught up to our time)
                    TimerEnded();
                }
            }
        }
    }

    public void SetToMax()
    {
        currentTime = duration;
        runTimer = true;
    }

    public void RestartTimer()
    {
        SetToMax();
        OnRestarted?.Invoke();
    }

    public void StartTimer()
    {
        SetToMax();
        OnStarted?.Invoke();
    }

    /// <summary>
    /// Fill in parameters where necessary.
    /// </summary>
    /// <param name="_duration">How long the timer should take.</param>
    /// <param name="_direction">False = Countdown | True = Stopwatch</param>
    /// <param name="_loop">Should the timer loop?</param>
    public void StartTimer(float _duration = 0f, bool _direction = false, bool _loop = false)
    {
        duration = _duration;
        timerDirection = _direction;
        loop = _loop;
        StartTimer();
    }

    private void TimerEnded()
    {
        currentTime = 0;
        OnEnded?.Invoke();
        runTimer = false;

        if (loop)
        {
            RestartTimer();
        }
    }

    public bool TimerRunning()
    {
        return runTimer;
    }

    public void TimerDecreases()
    {
        timerDirection = false;
    }

    public void TimerIncreases()
    {
        timerDirection = true;
    }

    public void StopTimer()
    {
        TimerEnded();
    }
}
