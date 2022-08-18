using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdQuest : QuestState
{
    public override void UpdateState(QuestManager questManager)
    {
         float dist = Vector3.Distance(questManager.gunShop.transform.position, questManager.player.transform.position);
        if (dist < questManager.range)
        {
            if (questManager.riflePickup.obj3 == true)
            {
                questManager.zombieSpawner.spawnZombies = true;
                questManager.objective3 = true;
                Debug.Log("Quest4 is trueeeeeeeeee");

            }

        }
        if (questManager.objective3 == true)
        {
            
            questManager.SwitchState(questManager.fourthQuest);

            Debug.Log("Quest4 is ongoing");
        }



    }

}