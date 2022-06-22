using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [Header("Damage")]
    public int damage;
    public ZombieMovement zombieMovement;
    public PlayerHealth playerHealth;

    [Header("Sound Effects")]
    public AudioSource melee;
    public AudioClip meleeClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //DamagePlayer();
    }
    public void DamagePlayer()
    {
        if (zombieMovement.inAttackRange)
        {
            playerHealth.TakeDamage(damage);
        }
    }
    private void PlayMeleeSoundEffect()
    {
        melee.PlayOneShot(meleeClip);
    }
}
