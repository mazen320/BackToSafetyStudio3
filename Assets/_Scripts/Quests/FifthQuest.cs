using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifthQuest : QuestState
{
    public override void UpdateState(QuestManager questManager)
    {
        float dist = Vector3.Distance(questManager.priestNpc.transform.position, questManager.player.transform.position);
        if (dist < questManager.range)
        {
            questManager.objective5 = true;
        }

        if(questManager.objective5 == true)
        {

            questManager.SwitchState(questManager.sixthQuest);

        }


    }

}