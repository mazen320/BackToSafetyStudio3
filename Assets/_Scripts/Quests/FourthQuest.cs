using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthQuest : QuestState
{
    

    public override void UpdateState(QuestManager questManager)
    {
        float dist = Vector3.Distance(questManager.gunShop.transform.position, questManager.player.transform.position);
        if (dist < questManager.range)
        {
            if (questManager.riflePickup.obj4 == true)
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