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

    public RiflePickup riflePickup;
    public PlayerScript playerScript;
    public bool callAnswered;
    public float range = 2f;
    public GameObject phone;
    public GameObject player;
    public GameObject gunShop;
    public GameObject house;
    public GameObject church;
    public GameObject storageYard;




    public bool objective1;
    public bool objective2;
    public bool objective3;
    public bool objective4;
    public bool objective5;
    public bool objective6;
    public bool objective7;
    public bool objective8;
    public bool objective9;
    public bool objective10;
    public bool objective11;
    public bool objective12;
    public bool objective13;


    public ZombieSpawner zombieSpawner;


    void Start()
    {
        phone = GameObject.Find("Phone");
        gunShop = GameObject.Find("GunShop");
        house = GameObject.Find("HouseObj");
        church = GameObject.Find("ChurchObj");
        storageYard = GameObject.Find("StorageYardObj");
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




