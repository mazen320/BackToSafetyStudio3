using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public string EnemyID { get; set; }
    //public ShootObject shootObject;
    


    public KillGoal(Quest quest, string enemyID, string description, bool isDone, int currentZombies, int requiredZombiesKills )
    {
        this.Quest = quest;
        this.EnemyID = enemyID;
        this.Description = description;
        this.IsDone = isDone;
        this.CurrentZombies = currentZombies;
        this.RequiredZombiesKills = requiredZombiesKills;

    }

    public override void Init()
    {
        base.Init();
    }

   public void ZombieDied()
    {
        //if(shootObject.zombiedied == true)
        {
            this.CurrentZombies++;
            Evaluate();
           // Debug.Log("ZOMBIEEEEEEEEEEEEEEEEEEE YES");
        }
    }

    
}
