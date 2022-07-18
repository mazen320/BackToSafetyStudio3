using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Quest : MonoBehaviour
{
    public FirstMissionState firstMissionState;
    public SecondMissionState secondMissionState;
    public ThirdMissionState thirdMissionState;
    public FourthMissionState fourthMissionState;
    public FifthMissionState fifthMissionState;
    public SixthMissionState sixthMissionState;
    public SeventhMissionState seventhMissionState;
    QuestState currentState;

    void Start()
    {
        

        firstMissionState = new FirstMissionState();
        secondMissionState = new SecondMissionState();
        thirdMissionState = new ThirdMissionState();
        fourthMissionState = new FourthMissionState();
        fifthMissionState = new FifthMissionState();
        sixthMissionState = new SixthMissionState();
        seventhMissionState = new SeventhMissionState();

         currentState = firstMissionState;
    }


    void Update()
    {

        
        currentState.UpdateState(this);



    }


    public void ChangeState(QuestState desiredState)
    {
        currentState = desiredState;
    }
    
    
}


public abstract class QuestState
{


    public abstract void UpdateState(Quest quest);

}

public class FirstMissionState : QuestState
{

    public override void UpdateState(Quest quest)
    {

        quest.ChangeState(Quest.SecondMissionState);
 
    }

    

}



public class SecondMissionState : QuestState
{
    


    public override void UpdateState(Quest quest)
    {


        quest.ChangeState(Quest.ThirdMissionState);


    }

 }


public class ThirdMissionState : QuestState
{



    public override void UpdateState(Quest quest)
    {


        quest.ChangeState(Quest.FourthMissionState);


    }

}

public class FourthMissionState : QuestState
{



    public override void UpdateState(Quest quest)
    {


        quest.ChangeState(Quest.FifthMissionState);


    }

}

public class FifthMissionState : QuestState
{



    public override void UpdateState(Quest quest)
    {


        quest.ChangeState(Quest.SixthMissionState);


    }

}

public class SixthMissionState : QuestState
{



    public override void UpdateState(Quest quest)
    {


        quest.ChangeState(Quest.SeventhMissionState);


    }

}

public class SeventhMissionState : QuestState
{



    public override void UpdateState(Quest quest)
    {


        quest.ChangeState(Quest.MissionState);


    }

}



