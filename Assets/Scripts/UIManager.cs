using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image stressMeter;
    public Text gameTime;
    public GameObject TaskListContent;
    public TaskUI taskUiPrefab;

    private static UIManager m_instance;
    public static UIManager Instance
    {
        get { return m_instance; }
    }

    void Awake()
    {
        m_instance = this;
    }

    private void Update()
    {
        TimeSpan ts = TimeSpan.FromSeconds(GameManager.Instance.timer);
        gameTime.text = ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
        stressMeter.fillAmount = StressManager.Instance.Stress;

        if (Input.GetKeyDown(KeyCode.A))
            StressManager.Instance.AddStress(1);
        if (Input.GetKeyDown(KeyCode.D))
            StressManager.Instance.SubtractStress (1);
    }

    public void OnAddTask(Task task)
    {
        TaskUI taskUi = Instantiate(taskUiPrefab, TaskListContent.transform.position, TaskListContent.transform.rotation) as TaskUI;
        taskUi.task = task;
        taskUi.toggle.isOn = false;
        taskUi.taskText.text = task.Name;
        taskUi.gameObject.SetActive(true);
        taskUi.transform.SetParent(TaskListContent.transform);
        taskUi.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnTaskComplete(Task task)
    {
        var taskList = TaskListContent.gameObject.GetComponentsInChildren<TaskUI>(true);
        for (int i = 0; i < taskList.Length; i++)
        {
            if (taskList[i].task == task)
            {
                taskList[i].toggle.isOn = true;
            }
        }
    }
}

