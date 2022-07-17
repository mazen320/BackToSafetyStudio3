using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMaybe : MonoBehaviour
{
    [Header("First test mission")]
    public ZombieSpawner zombieSpawner;
    public bool objective1;


    void Update()
    {
        if (zombieSpawner.killCount > 3)
        {
            objective1 = true;
        }
    }
}
