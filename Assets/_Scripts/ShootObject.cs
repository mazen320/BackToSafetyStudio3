using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObject : MonoBehaviour
{
    public float objectHealth = 100f;

    public void ObjectHitDamage(float dmg)
    {
        objectHealth -= dmg;
        Debug.Log("This zombie's health is " + objectHealth);
        if (objectHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
