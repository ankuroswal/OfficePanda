using UnityEngine;
using System.Collections.Generic;

public class Task  : MonoBehaviour
{
    public string Name;
    public List<Step> StepList;
    private int m_currentstep;

    public bool ProceedTask()
    {
        if (TaskComplete()) return false;
        m_currentstep++;
        return true;
    }

    public bool TaskComplete()
    {
        return StepList.Count >= m_currentstep;
    }

    public Step GetCurrentStep()
    {
        return StepList[m_currentstep];
    }  
}
