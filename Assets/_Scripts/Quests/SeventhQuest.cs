using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeventhQuest : QuestState
{
    float dist;
    public override void UpdateState(QuestManager questManager)
    {
        dist = Vector3.Distance(questManager.priestNpc.transform.position, questManager.player.transform.position);
        if (dist < questManager.range)
        {
            questManager.objective7 = true;
        }
        if (questManager.objective7 == true)
        {

            dist = Vector3.Distance(questManager.cityhall.transform.position, questManager.player.transform.position);
            if (dist < questManager.range)
            {
                questManager.objective8 = true;
            }
        }







    }

}