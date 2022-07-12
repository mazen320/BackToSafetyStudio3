using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [Header("HealthBar")]
    public Slider playerHealthSlider;
    public Image healthBarImage;
    public Gradient gradient;

    [Header("FadingInAndOut")]
    [SerializeField] CanvasGroup myGroup;
    public PlayerHealth playerHealth;
    public RifleManager rifleManager;
    public Transform rifle;

    private void Start()
    {
        myGroup = GameObject.Find("HealthAndInventoryCanvas").GetComponent<CanvasGroup>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        //rifleManager = GameObject.FindGameObjectWithTag("PlayerRifle").GetComponent<RifleManager>();
        rifleManager = rifle.GetComponent<RifleManager>();
    }

    public void SetMaxHealth(float health)
    {
        playerHealthSlider.maxValue = health;
        playerHealthSlider.value = health;
        healthBarImage.color = gradient.Evaluate(1f);//1f is the max value of the gradient. colour will correlate with the chosen colours relative to the gradient value.
    }
    public void SetHealth(float health)
    {
        playerHealthSlider.value = health;
        healthBarImage.color = gradient.Evaluate(playerHealthSlider.normalizedValue);//player health slider updates with the player's current health.
    }
    private void Update()
    {
        FadeIn();
        FadeOut();
    }
    private void FadeIn()
    {
        if (playerHealth.tookDamage || rifleManager.aiming)
        {
            if(myGroup.alpha < 1)
            {
                myGroup.alpha += Time.deltaTime * 3;
            }
        }
    }

    private void FadeOut()
    {
        if (playerHealth.tookDamage == false || playerHealth.currentRegenTimer <= 0)
        {
            if (myGroup.alpha >= 0)
            {
                myGroup.alpha -= Time.deltaTime;
            }
        }
    }
}
