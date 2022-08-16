using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixthQuest : QuestState
{
    PriestNpc priestNpc;
    public override void UpdateState(QuestManager questManager)
    {
        if(questManager.objective6 == true)
        {

        questManager.SwitchState(questManager.sevenQuest);

        }
        



    }

}