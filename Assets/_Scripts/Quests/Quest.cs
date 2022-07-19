using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Quest : MonoBehaviour
{
    FirstQuest firstQuest;
    SecondQuest secondQuest;
    ThirdQuest thirdQuest;


    QuestState currentState;


    void Start()
    {
        currentState.StartState(this);

         currentState = firstQuest;
    }


    void Update()
    {
        currentState.UpdateState(this);
    }


    public void SwitchState(QuestState state)
    {
        currentState = state;
        state.StartState(this);
    }


}




