using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Local Variables")]
    public float currentHealth;
    public float maxHealth;//For resetting health.
    public int regenRate;
    public float currentRegenTimer;
    public float regenTimer;//For resetting timer.
    public int healthGainAmount;
    public bool tookDamage;

    [Header("UI Managemnt")]
    public GameObject healthBar;
    public HealthBarManager healthBarManager;//Manages the UI for the healthbar.
    public DamageUIManager damageUIManager;
    public Menus menus;

    public AudioSource heal;
    public AudioClip healClip;

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
        currentRegenTimer -= Time.deltaTime;
        CheckRegenTimer();
        //RegenerateHealth(regenRate);
        Debug.Log("Player health is " + currentHealth);
        healthBarManager.playerHealthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; //* Time.deltaTime;
        tookDamage = true;
        currentRegenTimer = regenTimer;
        healthBarManager.SetHealth(currentHealth);
        PossiblyDie(currentHealth);//Player death function.
        damageUIManager.myGroup.alpha = 1;
    }

    private void CheckRegenTimer()//Whenever the player loses health somehow, either from an enemy or environment, bool tookDamage will be true.
    {
       /* if (tookDamage)
        {
            currentRegenTimer -= Time.deltaTime;
        }*/
        if (currentRegenTimer <= 0)
        {
            currentRegenTimer = 0;
            tookDamage = false;
        }
        else
        {
            tookDamage = true;
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
                //tookDamage = false;
            }
        }
    }
    public void GainHealth()
    {
        currentHealth += healthGainAmount;
        healthBarManager.SetHealth(currentHealth);
        tookDamage = false;
    }
    public void PossiblyDie(float healthLevel)
    {
        if(healthLevel <= 0)
        {
            menus.Pause();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "HealthPickUp" && currentHealth < maxHealth)
        {
            currentRegenTimer = regenTimer;
            GainHealth();
            heal.PlayOneShot(healClip);
            Destroy(other.gameObject);
        }
    }
}
