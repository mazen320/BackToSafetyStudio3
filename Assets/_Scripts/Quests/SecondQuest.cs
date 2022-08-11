using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondQuest : QuestState
{

    public override void UpdateState(QuestManager questManager)
    {
        if (questManager.playerScript.pickedAmmo == true)
        {
            questManager.objective2 = true;
            questManager.zombieSpawner.spawnZombies = true;
            Debug.Log("Quest2 is true");
        }
        if (questManager.objective2 == true)
        {
            questManager.SwitchState(questManager.thirdQuest);
            Debug.Log("Quest3 is ongoing");
        }
    }

}
