using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Waypoint : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI meter;
    public Vector3 offset;
 //   public Transform[] target;
    public GameObject target;
 /*   public int targetcount = 0;
    public float Range = 2f;*/

    public GameObject phone;
    public GameObject npc1;
    public GameObject ammoBox;
    public GameObject gunShop;
    public GameObject house;
    public GameObject church;
    public GameObject storageYard;

    public GameObject player;
   
    public QuestUi questUi;
    public QuestManager questManager;

 
    public void Start()
    {
        player = GameObject.Find("Player");

        phone = GameObject.Find("Phone");
        npc1 = GameObject.Find("NPCTwo");
        ammoBox = GameObject.Find("AmmoBoxObj");
        gunShop = GameObject.Find("GunShop");
        house = GameObject.Find("HouseObj");
        church = GameObject.Find("ChurchObj");
        storageYard = GameObject.Find("StorageYardObj");

        target = phone;

    }
    public void Update()
    {
        
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;

        Vector3 pos = Camera.main.WorldToScreenPoint(target.transform.position + offset);

        if(Vector3.Dot((target.transform.position - player.transform.position), transform.forward) < 0) 
        {
            if(pos.x < Screen.width / 2)// if the target is behind the player 
            {
                pos.x = maxX;
            }
            else
            {
                pos.x = minX;
            }
        }
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;
        meter.text = ((int)Vector3.Distance(target.transform.position, player.transform.position)).ToString() + "m";
        CheckTarget();
    }

        /*TargetCheck();*/
    /* public void TargetCheck()
     {
         currentTarget = target[targetcount];
         var distanceVector = Vector3.Distance(currentTarget.position, transform.position);
         if(distanceVector <= Range)
         {
             targetcount++;
             Debug.Log(targetcount + "TARGET COUNT");
         }

     }*/


    public void CheckTarget()
    {

        if (questManager.objective1 == true)
        {
            target = npc1;
        }

        if (questManager.objective2 == true)
        {

            target = gunShop;
        }

        if (questManager.objective3 == true)
        {
            target = house;
        }
       
        if(questManager.objective4 == true)
        {
            target = church;
        }

        if(questManager.objective5 == true)
        {
            target = storageYard;

        }

        if (questManager.objective6 == true)
        {
            target = church;
        }


    }
}
