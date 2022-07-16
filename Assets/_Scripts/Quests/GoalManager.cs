using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class GoalManager : MonoBehaviour
    {

       /* public Goal[] goals;

        void Awake()
        {
            goals = GetComponents<Goal>();
        }

        void OnGUI()
        {
            foreach (var goal in goals)
            {
                goals.DrawHUD();
            }
        }

        void Update()
        {
            foreach (var goal in goals)
            {
                if (goal.IsAchieved())
                {
                    goal.Complete();
                    Destroy(goal);
                }
            }
        }
    }



// This is the abstract base class for all goals:
public abstract class Goal : MonoBehaviour
{ 
    public abstract bool Complete();
}


// This is the First goal
public class Touchobject : Goal
{
    public int objecttouched = 0;
    public int requiredTouch = 1;

    public bool Touched = false;

   *//* public override bool IsComplete()
    {
        return (objecttouched >= requiredTouch);
    }
*//*
    public override void Complete()
    {
        ScoreSingleton.score += 1;
    }
    *//*public override bool IsAchieved()
    {
        return (playerStats.kills >= requiredKills);
    }*//*

    public override void DrawHUD()
    {
        GUILayout.Label(string.Format("Collected {0}/{1} objects", objecttouched, requiredTouch));

    }

    
    public void OnTriggerEnter(Collider other)
    {
        if (string.Equals(other.tag, "Object"))
        {
            objecttouched++;
            Destroy(other.gameObject);
        }
    }*/
}


