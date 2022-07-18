using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest : QuestState
{
    public override void StartState(Quest quest)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(Quest manager)
    {
        manager.SwitchState(new SecondQuest());
    }

}
