using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    FirstQuest firstQuest;
    SecondQuest secondQuest;
    ThirdQuest thirdQuest;
    FourthQuest fourthQuest;
    FifthQuest fifthQuest;
    SixthQuest sixthQuest;
    SeventhQuest sevenQuest;

     QuestState currentState;

    public ZombieSpawner zombieSpawner;
    public bool objective1;
    public bool objective2;


    void Start()
    {
        zombieSpawner = GetComponent<ZombieSpawner>();
        firstQuest = new FirstQuest();
        secondQuest = new SecondQuest();
        thirdQuest = new ThirdQuest();
        //  currentState.StartState(this);
        currentState = firstQuest;



    }


    void Update()
    {
        currentState.UpdateState(this);
       // Debug.Log("CURRENT QUEST IS " + currentState);
    }


    public void SwitchState(QuestState questState)
    {
        currentState = questState;
        //questState.StartState(this, questState);
    }

    public abstract class QuestState
    {
        
        public abstract void UpdateState(QuestManager quest);

    }




    public class FirstQuest : QuestState
    {

        /*public override void StartState(QuestManager questManager, QuestState questState)
        {
            questManager.currentState = questState;

        }*/

        public override void UpdateState(QuestManager questManager)
        {
            if (questManager.zombieSpawner.killCount >= 1)
            {
                questManager.objective1 = true;
                Debug.Log("Quest1 is true");
            }
            if (questManager.objective1 == true)
            {
                questManager.SwitchState(new SecondQuest());
                Debug.Log("Quest2 is ongoing");

            }
        }

    }

    public class SecondQuest : QuestState
    {
        
       /* public override void StartState(QuestManager questManager, QuestState questState)
        {
            questManager.currentState = questState;
        }*/

        public override void UpdateState(QuestManager questManager)
        {
            if (questManager.zombieSpawner.killCount >= 2)
            {
               questManager.objective2 = true;
                Debug.Log("Quest2 is true");
            }
            if (questManager.objective2 == true)
            {
                //  questManager.SwitchState(new ThirdQuest());
                Debug.Log("Quest3 is ongoing");
            }
        }

    }
    public class ThirdQuest : QuestState
    {

        

        public override void UpdateState(QuestManager questManager)
        {
            
            
                questManager.SwitchState(new FourthQuest());
                
            
        }

    }
    public class FourthQuest : QuestState
    {



        public override void UpdateState(QuestManager questManager)
        {

            
                questManager.SwitchState(new FifthQuest());

            
        }

    }
    public class FifthQuest : QuestState
    {



        public override void UpdateState(QuestManager questManager)
        {

            
                questManager.SwitchState(new SixthQuest());

            
        }

    }
    public class SixthQuest : QuestState
    {



        public override void UpdateState(QuestManager questManager)
        {

            
                questManager.SwitchState(new SeventhQuest());

            
        }

    }
    public class SeventhQuest : QuestState
    {



        public override void UpdateState(QuestManager questManager)
        {

            
                

            
        }

    }
}




