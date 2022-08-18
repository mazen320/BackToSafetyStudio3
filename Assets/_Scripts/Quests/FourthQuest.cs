using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthQuest : QuestState
{
    

    public override void UpdateState(QuestManager questManager)
    {
        float dist = Vector3.Distance(questManager.house.transform.position, questManager.player.transform.position);
        if (dist < questManager.range)
        {
            questManager.objective4 = true;
        }


        if (questManager.objective4 == true)
        {

            questManager.SwitchState(questManager.fifthQuest);


        }

    }
}