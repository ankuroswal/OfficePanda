
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    public Task task;
    public Text taskText;
    public Toggle toggle;

    public void Awake()
    {
        taskText = GetComponentInChildren<Text>();
        toggle = GetComponent<Toggle>();
    }

}

