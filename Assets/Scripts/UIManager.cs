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

    void Update()
    {
        gameTime.text = "Day: " + GameManager.Instance.day;
        stressMeter.fillAmount = GameManager.Instance.StressManager.Stress;
    }

    public void OnAddTask(Task task)
    {
        TaskUI taskUi = Instantiate(taskUiPrefab, TaskListContent.transform.position, TaskListContent.transform.rotation) as TaskUI;
        taskUi.task = task;
        taskUi.taskImages = task.taskImages;
        taskUi.SetImageByIndex(0);
        taskUi.gameObject.SetActive(true);
        taskUi.transform.SetParent(TaskListContent.transform);
        taskUi.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnProceedTask(Task task)
    {
        var taskList = TaskListContent.gameObject.GetComponentsInChildren<TaskUI>(true);
        for (int i = 0; i < taskList.Length; i++)
        {
            if (taskList[i].task == task)
            {
                taskList[i].SetImageByIndex(task.CurrentStep);
            }
        }
    }

    public void OnTaskComplete(Task task)
    {
        var taskList = TaskListContent.gameObject.GetComponentsInChildren<TaskUI>(true);
        for (int i = 0; i < taskList.Length; i++)
        {
            //if (taskList[i].task == task)
            //{
            //    taskList[i].toggle.isOn = true;
            //}
        }
    }
}

