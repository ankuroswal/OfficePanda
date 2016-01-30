// ---------------------------------------------------------------------------------------------------------------------
// - Confidential Information                                                                                          
// - Copyright 20#YEARSHORT#, Obsidian Entertainment, Inc.                                                             
// - All rights reserved.  
// - Created by: #AUTHOR# on #DATE#                                                                                    
// ---------------------------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class ActionEdge 
{
    public ActionEvent StartAction;
    public ActionEvent EndAction;

    public ActionEdge(ActionEvent start, ActionEvent end)
    {
        StartAction = start;
        EndAction = end;
    }

    public bool isEqual(ActionEdge other)
    {
        return StartAction.EventName == other.StartAction.EventName &&
               EndAction.EventName == other.EndAction.EventName;
    }
}
