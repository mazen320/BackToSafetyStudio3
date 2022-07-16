using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal  
{
    public Quest Quest { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; }
    public int CurrentZombies { get; set; }
    public int RequiredZombiesKills { get; set; }


    public virtual void Init()
    {

    }

    public void Evaluate()
    {
        if(CurrentZombies >= RequiredZombiesKills)
        {
            Complete();
        }
    }


    public void Complete()
    {
        Quest.CheckGoals();
        IsDone = true;
        
    }
}

