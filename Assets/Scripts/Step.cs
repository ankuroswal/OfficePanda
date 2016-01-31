using UnityEngine;
using System.Collections;

public class Step : MonoBehaviour
{
    public ActionEvent Start;
    public ActionEvent End;
    public string StepName;
    
    public ActionEdge GetEdge()
    {
        return new ActionEdge(Start, End);
    }
}