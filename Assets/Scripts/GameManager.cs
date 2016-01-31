using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    public List<Task> TaskList;
    public StressManager StressManager;
    public MusicManager MusicManager;
    public UIManager UIManager;
    public Animator CameraAnim;
    public GameObject MainMenuScreen;
    public GameObject GameScreen;

    public int day = 0;
    public bool gameStart = false;
    public Task debugTask;

    public static GameManager Instance
    {
        get { return m_instance; }
    }

    private static GameManager m_instance;
    private int m_currentTask;

    void Awake()
    {
        Cursor.visible = true;
        gameStart = false;
        CameraAnim = Camera.main.GetComponent<Animator>();
        CameraAnim.SetBool("FirstDay", true);
        DontDestroyOnLoad(transform.gameObject);   
    }

    void Start()
    {
        MainMenuScreen.SetActive(true);
        GameScreen.SetActive(false);
        m_instance = this;
        m_currentTask = 0;
        //debugTask.CurrentStep = 0;
        //AddTask(debugTask);
    }

    public void PlayGame()
    {
        CameraAnim.SetTrigger("Play");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(3f);
        MainMenuScreen.SetActive(false);
        GameScreen.SetActive(true);
        CameraAnim.SetBool("FirstDay", false);
        gameStart = true;
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

    public void ShowDayScene()
    {
        SceneManager.LoadScene("Day");
    }

    public void ShowOfficeScene()
    {
        SceneManager.LoadScene("Office");
        day++;
    }
}
