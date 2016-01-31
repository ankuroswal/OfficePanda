using UnityEngine;
using System.Collections.Generic;

public class StressManager  : MonoBehaviour
{
    // PUBLIC
    public float StressCountTime = 1.0f;
    public float MaximumStress = 10.0f;

    // PRIVATE
    private static StressManager m_instance;
    private float m_timer = 0.0f;
    private float m_currentStress = 0.0f;

    public float Stress
    {
        get { return (m_currentStress / MaximumStress); }
    }

    public static StressManager Instance
    {
        get { return m_instance; }
    }

    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {

    }

    void Update()
    {
        m_timer += Time.deltaTime;
        if(m_timer >= StressCountTime)
        {
            m_timer -= StressCountTime;
            List<Task> tasks = GameManager.Instance.TaskList;
            if (tasks != null)
            {
                for(int i = 0; i < tasks.Count; i++)
                    m_currentStress += (tasks[i].StressAmount);
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
