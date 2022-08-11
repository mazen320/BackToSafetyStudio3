using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdQuest : QuestState
{
    public override void UpdateState(QuestManager questManager)
    {
        if (questManager.playerScript.pickedAmmo == true)
        {
            questManager.objective3 = true;
            questManager.zombieSpawner.spawnZombies = true;
            Debug.Log("Quest3 is true");
        }
        if (questManager.objective3 == true)
        {
            
            questManager.SwitchState(questManager.fourthQuest);

            Debug.Log("Quest4 is ongoing");
        }



    }

}