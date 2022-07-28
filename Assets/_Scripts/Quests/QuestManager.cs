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

    public PlayerScript player;
    public bool callAnswered;
    public float range = 2f;
    public GameObject phone;

    


    public bool objective1;
    public bool objective2;

    ZombieSpawner zombieSpawner;
    void Start()
    {
        phone = GameObject.Find("Phone");
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
                questManager.SwitchState(new SecondQuest());
                Debug.Log("Quest2 is ongoing");

            }
        }

    }

    public class SecondQuest : QuestState
    {

        

        public override void UpdateState(QuestManager questManager)
        {
            if (questManager.player.pickedAmmo == true)
            {
               questManager.objective2 = true;
                questManager.zombieSpawner.spawnZombies = true;
                Debug.Log("Quest2 is true");
            }
            if (questManager.objective2 == true)
            {
                questManager.SwitchState(new ThirdQuest());
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




