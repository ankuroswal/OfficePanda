using UnityEngine;
using System.Collections.Generic;

public class Task  : MonoBehaviour
{
    public string Name;
    public float StressAmount = 1.0f;
    public float ReliefAmount = 10.0f;
    public Step[] StepList;
    private int m_currentstep;

    public bool ProceedTask()
    {
        if (TaskComplete()) return false;
        m_currentstep++;
        return true;
    }

    public bool TaskComplete()
    {
        return StepList.Length >= m_currentstep;
    }

    public Step GetCurrentStep()
    {
        return StepList[m_currentstep];
    }

    public void RelieveStress()
    {
        StressManager.Instance.RelieveStress(ReliefAmount);
    }
}
