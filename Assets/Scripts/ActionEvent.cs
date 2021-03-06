﻿// ---------------------------------------------------------------------------------------------------------------------
// - Confidential Information                                                                                          
// - Copyright 20#YEARSHORT#, Obsidian Entertainment, Inc.                                                             
// - All rights reserved.  
// - Created by: #AUTHOR# on #DATE#                                                                                    
// ---------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System;

public class ActionEvent : MonoBehaviour 
{
    public string EventName;
    public Collider Collider;
    public float SecondsToHappen;
    public bool ParentEvent;

    public Action<bool> OnAction;

    private ActionEvent m_currentEvent;
    private bool m_triggerEvent;
    private float m_timecount;

    
    void Start()
    {
        Init();
    }

    void OnTriggerEnter(Collider other)
    {
        ActionEvent interactable = other.GetComponent<ActionEvent>();

        if (interactable != null)
        {
            if (ParentEvent)
            {
                Init();
                m_currentEvent = interactable;
                m_triggerEvent = true;
                OnAction += ActionTrigger;
            }
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
                m_triggerEvent = false;
                OnAction(true);
            }
            m_timecount += Time.deltaTime;
        }
    }

    private void ActionTrigger(bool Action)
    {
        GameManager.Instance.ActionEventNotify(new ActionEdge(this, m_currentEvent));
    }

    private void Init()
    {
        m_triggerEvent = false;
        m_timecount = 0;
        m_currentEvent = null;
    }
}
