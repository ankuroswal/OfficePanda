using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
    public List<Task> TaskList;
    public StressManager StressManager;
    public MusicManager MusicManager;
    public UIManager UIManager;
    public AudioManager AudioManager;

    public float timer;

    public Task debugTask;

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
        MusicManager.StartPlaying();
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public void AddTask(Task task)
    {
        TaskList.Add(task);
        UIManager.OnAddTask(task);
    }
    
    public void ActionEventNotify(ActionEdge notifiedEdge)
    {
        ActionEdge currentEdge = TaskList[m_currentTask].GetCurrentStep().GetEdge();
        if (currentEdge.isEqual(notifiedEdge))
        {
            Debug.Log(notifiedEdge.StartAction.EventName + "---" + notifiedEdge.EndAction.EventName);
            TaskList[m_currentTask].ProceedTask();
        }
    }
}
