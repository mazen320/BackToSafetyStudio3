using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;//For resetting health.
    public int regenRate;
    public float currentRegenTimer;
    public float regenTimer;//For resetting timer.
    public bool tookDamage;

    public GameObject healthBar;
    public HealthBarManager healthBarManager;//Manages the UI for the healthbar.

    public Menus menus;

    // Start is called before the first frame update
    private void Awake()
    {
        maxHealth = 100f;
        regenRate = 5;
        regenTimer = 3f;
    }
    void Start()
    {
        currentHealth = maxHealth;
        currentRegenTimer = regenTimer;

        healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        healthBarManager = healthBar.GetComponent<HealthBarManager>();
        healthBarManager.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (currentHealth <= 40)
        {
            currentHealth = 40;
        }*/

        StartRegenTimer();
        RegenerateHealth(regenRate);
        Debug.Log("Player health is " + currentHealth);
        healthBarManager.playerHealthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage * Time.deltaTime;
        tookDamage = true;
        currentRegenTimer = regenTimer;
        healthBarManager.SetHealth(currentHealth);
        PossiblyDie(currentHealth);//Player death function.
    }

    private void StartRegenTimer()//Whenever the player loses health somehow, either from an enemy or environment, bool tookDamage will be true.
    {
        if (tookDamage)
        {
            currentRegenTimer -= Time.deltaTime;
        }
        if (currentRegenTimer <= 0)
        {
            currentRegenTimer = 0;
        }
    }

    public void RegenerateHealth(int regenerationSpeed)
    {
        if (currentRegenTimer == 0)
        {
            currentHealth += regenerationSpeed * Time.deltaTime;
            healthBarManager.SetHealth(currentHealth);
            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
                tookDamage = false;
            }
        }
    }
    public void PossiblyDie(float healthLevel)
    {
        if(healthLevel <= 0)
        {
            menus.Pause();
        }
    }
}
