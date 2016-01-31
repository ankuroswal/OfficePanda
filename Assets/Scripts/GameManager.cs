using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public List<Task> TaskList;
    public float timer;

    public static GameManager Instance
    {
        get { return m_instance; }
    }

    private static GameManager m_instance;
    private int m_currentTask;
    private bool gameInProgress;

    void Start()
    {
        m_instance = this;
        m_currentTask = 0;
    }

    private void Update()
    {
        //if (!gameInProgress) return;

        timer += Time.deltaTime;
    }

    public void AddTask(Task task)
    {
        TaskList.Add(task);
        UIManager.Instance.OnAddTask(task);
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
