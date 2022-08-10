using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixthQuest : QuestState
{
    public override void UpdateState(QuestManager questManager)
    {


        questManager.SwitchState(questManager.sevenQuest);


    }

}