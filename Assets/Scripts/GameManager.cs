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
    public AudioManager AudioManager;

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

        if (DayTracker.currentDay == 0)
            CameraAnim.SetBool("FirstDay", true);
        else
            CameraAnim.SetBool("FirstDay", false);
    }

    void Start()
    {
        if (DayTracker.currentDay == 0)
        {
            MainMenuScreen.SetActive(true);
            GameScreen.SetActive(false);
        }
        else if (DayTracker.currentDay < 5)
        {
            MainMenuScreen.SetActive(false);
            GameScreen.SetActive(true);
            gameStart = true;
        }
        else
        {
            Restart();
        }

        for (int i = 0; i < DayTracker.currentDay + 1; i++)
        {
            AddTask(TaskList[i]);
        }
        MusicManager.StartPlaying();

        m_instance = this;
        m_currentTask = 0;
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
        //TaskList.Add(task);
        UIManager.OnAddTask(task);
    }
    
    public void ActionEventNotify(ActionEdge notifiedEdge)
    {
        if (!gameStart) return;
        if (TaskList.Count <= m_currentTask) return;

        ActionEdge currentEdge = TaskList[m_currentTask].GetCurrentStep().GetEdge();
        if (currentEdge.isEqual(notifiedEdge))
        {
            TaskList[m_currentTask].ProceedTask();
            if (TaskList[m_currentTask].TaskComplete())
            {
                m_currentTask++;
                if (DayTracker.currentDay < m_currentTask)
                {
                    ShowDayScene();
                }
            }
        }
    }

    public void ShowDayScene()
    {
        SceneManager.LoadScene("Sunset");
    }

    public void Restart()
    {
        StartCoroutine(Re());
    }

    public IEnumerator Re()
    {
        yield return new WaitForSeconds(3f);
        DayTracker.currentDay = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("yoloscene2");
    }


}
