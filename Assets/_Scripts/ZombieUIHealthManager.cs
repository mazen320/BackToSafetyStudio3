using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieUIHealthManager : MonoBehaviour
{
    public Slider zombieHealthSlider;
    public Image zombieHealthBarImage;
    public Gradient gradient;

    public ZombieSpawner zombieSpawner;

    public ShootObject zombieHealth;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < zombieSpawner.zombies.Count; i++)
        {
            zombieHealthSlider = zombieSpawner.zombies[i].GetComponent<Slider>();
            zombieHealth = zombieSpawner.zombies[i].GetComponent<ShootObject>();
        }
        foreach (var zombie in zombieSpawner.zombies)
        {
            SetZombieMaxHealth(zombieHealth.currentHealth);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FindZombieHealthBars()
    {

    }
    public void SetZombieMaxHealth(float health)
    {
        zombieHealthSlider.maxValue = health;
        zombieHealthSlider.value = health;
        zombieHealthBarImage.color = gradient.Evaluate(1f);//1f is the max value of the gradient. colour will correlate with the chosen colours relative to the gradient value.
    }
    public void SetZombieHealth(float health)
    {
        zombieHealthSlider.value = health;
        zombieHealthBarImage.color = gradient.Evaluate(zombieHealthSlider.normalizedValue);//player health slider updates with the zombie's current health.
    }
}
