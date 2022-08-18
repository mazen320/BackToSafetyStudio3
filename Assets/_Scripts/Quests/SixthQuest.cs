using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixthQuest : QuestState
{
    
    public override void UpdateState(QuestManager questManager)
    {
        
        if (questManager.vehicleController.isOpened == true)
        {
            questManager.objective6 = true;
        }

        if (questManager.objective6 == true)
        {

        questManager.SwitchState(questManager.sevenQuest);

        }
        



    }

}