// ---------------------------------------------------------------------------------------------------------------------
// - Confidential Information                                                                                          
// - Copyright 20#YEARSHORT#, Obsidian Entertainment, Inc.                                                             
// - All rights reserved.  
// - Created by: #AUTHOR# on #DATE#                                                                                    
// ---------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class ActionEvent : MonoBehaviour 
{
    public string EventName;
    public Collider Collider;
    public float SecondsToHappen;
    public bool ParentEvent;

    private ActionEvent m_currentEvent;
    private bool m_triggerEvent;
    private float m_timecount;

    void Start()
    {
        Init();
    }

    void OnTriggerEnter(Collider other)
    { 
        if (ParentEvent)
        {
            Init();
            m_currentEvent = other.GetComponent<ActionEvent>();
            m_triggerEvent = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Init();
    }

    void Update()
    {
        if (m_triggerEvent && m_currentEvent)
        {
            if (m_timecount > SecondsToHappen)
            {
                GameManager.Instance.ActionEventNotify(new ActionEdge(this, m_currentEvent));
                m_triggerEvent = false;
            }
            m_timecount += Time.deltaTime;
        }
    }

    private void Init()
    {
        m_triggerEvent = false;
        m_timecount = 0;
        m_currentEvent = null;
    }
}
