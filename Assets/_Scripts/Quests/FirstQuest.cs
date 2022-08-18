using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest : QuestState
{
    public override void UpdateState(QuestManager questManager)
    {
  

        if (Vector3.Distance(questManager.phone.transform.position, questManager.player.transform.position) < questManager.range)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                questManager.callAnswered = true;
                questManager.objective1 = true;
                Debug.Log("Quest1 is true");
            }

        }
        if (questManager.objective1 == true)
        {
            questManager.SwitchState(questManager.secondQuest);
            Debug.Log("Quest2 is ongoing");

        }
    }

}

