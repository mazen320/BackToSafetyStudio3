using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest : QuestState
{
    public ZombieSpawner zombieSpawner;
    public bool objective1;
    public override void StartState(Quest quest)
    {
        if(zombieSpawner.killCount >= 1)
        {
            objective1 = true;
        }
    }

    public override void UpdateState(Quest quest)
    {
        if (objective1 == true)
        {
            quest.SwitchState(new SecondQuest());
        }
    }

}
