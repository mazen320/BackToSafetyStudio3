using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    public FirstQuest firstQuest = new FirstQuest();
    public SecondQuest secondQuest = new SecondQuest();
    public ThirdQuest thirdQuest = new ThirdQuest();
    public FourthQuest fourthQuest = new FourthQuest();
    public FifthQuest fifthQuest = new FifthQuest();
    public SixthQuest sixthQuest = new SixthQuest();
    public SeventhQuest sevenQuest = new SeventhQuest();

    QuestState currentState;

    public PlayerScript player;
    public bool callAnswered;
    public float range = 2f;
    public GameObject phone;

    public bool objective1;
    public bool objective2;


    public ZombieSpawner zombieSpawner;


    void Start()
    {
        phone = GameObject.Find("Phone");
        currentState = firstQuest;

    }


    void Update()
    {
        currentState.UpdateState(this);
       
    }


    public void SwitchState(QuestState questState)
    {
        currentState = questState;
      
    }

}




