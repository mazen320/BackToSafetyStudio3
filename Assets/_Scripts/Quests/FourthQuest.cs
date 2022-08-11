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
            if (questManager.riflePickup.obj4)
            {

            questManager.objective4 = true;
            Debug.Log("Quest4 is trueeeeeeeeee");

            }

        }

        if (questManager.objective4 == true)
        {

            questManager.SwitchState(questManager.fifthQuest);


        }

    }
}