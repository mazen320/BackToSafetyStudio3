using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Quest : MonoBehaviour
{
    public List<Goal> Goals { get; set; } = new List<Goal>();

    public string Questname;
    public string Description;

    public bool IsDone;

    public void CheckGoals()
    {
        IsDone = Goals.All(g => g.IsDone);


    }
}
