using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondQuest : QuestState
{
    public ZombieSpawner ZombieSpawner;
    public bool objective2;
    public override void StartState(Quest quest)
    {
        if(ZombieSpawner.killCount >= 2)
        {
            objective2 = true;
        }
       
    }

    public override void UpdateState(Quest quest)
    {
        if (objective2 == true)
        {
            quest.SwitchState(new ThirdQuest());
        }
    }

}
