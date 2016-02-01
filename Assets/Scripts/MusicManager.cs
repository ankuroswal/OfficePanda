using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    // PUBLIC
    public AudioClip NormalTrack;
    public AudioClip StressTrack;
    public float DesiredVolume = 1.0f;

    // PRIVATE
    private AudioSource m_normalSource;
    private AudioSource m_stressSource;
    private bool m_canPlay = false;
    private static MusicManager m_instance;

    public static MusicManager Instance
    {
        get { return m_instance; }
    }

    void Awake()
    {
        m_instance = this;
        AudioSource[] sources = Camera.main.gameObject.GetComponents<AudioSource>();
        Debug.Log(sources.Length);
        //if(sources.Length >= 2)
        //{
        //    Debug.LogError("Did NOT find 2 audio sources on the main camera object!");
        //    return;
        //}
        m_normalSource = sources[0];
        m_stressSource = sources[1];
        m_canPlay = true;

    }

    private void Init()
    {
        if (!m_canPlay) return;
        m_normalSource.clip = NormalTrack;
        m_normalSource.volume = 1.0f;
        m_normalSource.loop = true;

        m_stressSource.clip = StressTrack;
        m_stressSource.volume = 0.0f;
        m_normalSource.loop = true;
    }

    public void StartPlaying()
    {
        if (!m_canPlay) return;
        Init();
        m_normalSource.Play();
        m_stressSource.Play();
    }

    public void StopPlaying()
    {
        if (!m_canPlay) return;
        m_normalSource.Stop();
        m_stressSource.Stop();
    }

    void Update()
    {
        if (!m_canPlay) return;
        m_normalSource.volume = 1.0f - GameManager.Instance.StressManager.Stress;
        m_stressSource.volume = GameManager.Instance.StressManager.Stress;
    }
}
