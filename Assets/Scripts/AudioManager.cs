// ---------------------------------------------------------------------------------------------------------------------
// - Confidential Information                                                                                          
// - Copyright 20#YEARSHORT#, Obsidian Entertainment, Inc.                                                             
// - All rights reserved.  
// - Created by: #AUTHOR# on #DATE#                                                                                    
// ---------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour 
{
    public AudioClip[] AudioClips;
    public AudioSource AudioSource;

    public void PlayAudioClip(int audioClipIndex)
    {
        AudioSource.Stop();
        AudioSource.volume = 1.0f;
        AudioSource.loop = false;
        AudioSource.clip = AudioClips[audioClipIndex];
        AudioSource.Play();
    }

}
