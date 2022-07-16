using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingZombies : Quest
{
    
    void Start()
    {
        Questname = "Kill Zombies";
        Description = "Kill 3 Zombies";

        Goals.Add(new KillGoal(this, "Zombie" , "KILL 4 ZOMBIES", false, 0, 1));

        Goals.ForEach(g => g.Init());
    }

    
}
