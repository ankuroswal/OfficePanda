// ---------------------------------------------------------------------------------------------------------------------
// - Confidential Information                                                                                          
// - Copyright 20#YEARSHORT#, Obsidian Entertainment, Inc.                                                             
// - All rights reserved.  
// - Created by: #AUTHOR# on #DATE#                                                                                    
// ---------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class PlayAudioOnAction : MonoBehaviour 
{
    public int AudioIndex;

    private ActionEvent m_event;

    public void Awake()
    {
        m_event = GetComponent<ActionEvent>();
        m_event.OnAction += PlayAudio;
    }

    private void PlayAudio(bool shouldPlay)
    {
        GameManager.Instance.AudioManager.PlayAudioClip(AudioIndex);
    }
}
