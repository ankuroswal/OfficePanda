using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StressManager : MonoBehaviour
{
    // PUBLIC
    public float StressCountTime = 10.0f;
    public float MaximumStress = 300.0f;
    public Text StressLabel;

    // PRIVATE
    private float m_timer = 0.0f;
    private float m_currentStress = 0.0f;

    public float Stress
    {
        get { return (m_currentStress / MaximumStress); }
    }

    void Start()
    {

    }

    public void SubtractStress(float amount)
    {
        m_currentStress -= amount;
    }

    public void AddStress(float amount)
    {
        m_currentStress += amount;
    }

    void Update()
    {
        if (!GameManager.Instance.gameStart) return;

        m_timer += Time.deltaTime;
        if(m_timer >= StressCountTime)
        {
            m_timer -= StressCountTime;
            List<Task> tasks = GameManager.Instance.TaskList;
            if (tasks != null)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    if (!tasks[i].TaskComplete())
                        AddStress(tasks[i].StressAmount);
                }
            }
        }
    }

    public void RelieveStress(float amount)
    {
        m_currentStress -= amount;
        if (m_currentStress < 0.0f)
            m_currentStress = 0.0f;
    }
}
