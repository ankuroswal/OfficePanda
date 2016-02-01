using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Task  : MonoBehaviour
{
    public string Name;
    public float StressAmount = 1.0f;
    public float ReliefAmount = 10.0f;
    public List<Step> StepList;
    public int AudioClip = -1;

    public Sprite[] taskImages;

    private int m_currentStep;

    public int CurrentStep
    {
       get { return m_currentStep; }
       set { m_currentStep = value;}
    }

    public void StartTask()
    {
        m_currentStep = 0;
        if (AudioClip > 0)
        {
            GameManager.Instance.AudioManager.PlayAudioClip(AudioClip);
        }
    }

    public bool ProceedTask()
    {
        CurrentStep++;
        GameManager.Instance.UIManager.OnProceedTask(this);
        if (TaskComplete())
        {
            GameManager.Instance.UIManager.OnTaskComplete(this);
            return false;
        }

        return !TaskComplete();
    }

    public bool TaskComplete()
    {
        return StepList.Count <= CurrentStep;
    }

    public Step GetCurrentStep()
    {
        return StepList[CurrentStep];
    }

    public void RelieveStress()
    {
        GameManager.Instance.StressManager.RelieveStress(ReliefAmount);
    }
}
