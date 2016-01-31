
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public Task task;
    public Sprite[] taskImages;
    public Image currentDisplayedTask;

    public void Awake()
    {
    }

    public void SetImageByIndex(int index)
    {
        if (taskImages.Length > index)
        {
            currentDisplayedTask.sprite = taskImages[index];
        }
    }

}

