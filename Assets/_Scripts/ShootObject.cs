using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObject : MonoBehaviour
{
    public float currentHealth;
    public ZombieUIHealthManager zombieUIHealthManager;
    public GameObject damageText;
    public float maxHealth;

    public Vector3 spawnOffset;//For spawning damage numbers.


    private void Awake()
    {
        currentHealth = maxHealth;
        /*damageText = GameObject.Find("RifleDamage");
        zombieUIHealthManager = FindObjectOfType<ZombieUIHealthManager>();*/
    }
    private void Update()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void ZombieTakeDamage(float dmg)
    {
        currentHealth -= dmg;
        Debug.Log("This zombie's health is " + currentHealth);
        DamageIndicator damageIndicator = Instantiate(damageText, transform.position + spawnOffset, Quaternion.identity).GetComponent<DamageIndicator>();
        damageIndicator.SetDamageText(dmg);
        zombieUIHealthManager.SetZombieHealth(currentHealth);
        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    public void SetUpHealthBar(Canvas canvas, Camera camera)
    {
        zombieUIHealthManager.transform.SetParent(canvas.transform);
    }
    void Die()
    {
        Destroy(gameObject, 1f);
        Destroy(zombieUIHealthManager.gameObject, 1f);
    }
}
