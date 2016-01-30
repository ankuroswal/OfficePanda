using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public List<Task> TaskList;
    public static GameManager Instance
    {
        get { return m_instance; }
    }

    private static GameManager m_instance;
    private int m_currentTask;

    void Start()
    {
        m_instance = this;
        m_currentTask = 0;
    }
    
    public void ActionEventNotify(ActionEdge notifiedEdge)
    {
        Debug.Log(notifiedEdge.StartAction.EventName + "---" + notifiedEdge.EndAction.EventName);
        ActionEdge currentEdge = TaskList[m_currentTask].GetCurrentStep().GetEdge();
        if (currentEdge.isEqual(notifiedEdge))
        {
            TaskList[m_currentTask].ProceedTask();
        }
    }
}
