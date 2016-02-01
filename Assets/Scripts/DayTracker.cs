
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayTracker : MonoBehaviour
{
    public static int currentDay = 0;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static void ShowOfficeScene()
    {
        SceneManager.LoadScene("yoloscene2");
        currentDay++;
    }
}

