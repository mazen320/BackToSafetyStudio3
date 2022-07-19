using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthQuest : QuestState
{
    public override void StartState(Quest quest)
    {
        
    }

    public override void UpdateState(Quest quest)
    {
        quest.SwitchState(new FifthQuest());
    }
}

