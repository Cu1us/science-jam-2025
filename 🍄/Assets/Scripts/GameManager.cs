using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] IntReference CurrentDay;
    [SerializeField] GameEvent OnDayProgressed;
    [SerializeField] IntReference RemainingSteps;
    [SerializeField] int[] MaxStepsPerDay;
    public void FadeToNextDay()
    {
        CurrentDay.Value++;
        OnDayProgressed.Invoke();
        RefillSteps();
    }

    void RefillSteps()
    {
        int maxSteps = 0;
        if (CurrentDay.Value < MaxStepsPerDay.Length)
        {
            maxSteps = MaxStepsPerDay[CurrentDay.Value];
        }
        RemainingSteps.Value = maxSteps;
    }
}
