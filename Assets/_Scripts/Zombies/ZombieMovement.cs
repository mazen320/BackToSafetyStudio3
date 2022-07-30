using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public Transform player;
    public Animator anim;

    [Header("Running")]
    public float runningSpeed;
    public float chaseRange;

    [Header("Attacking")]
    public float attackingSpeed;

    public float attackRange;

    public bool inAttackRange;
    public bool inChaseRange;

    [Header("Souund Effects")]
    public AudioSource footSteps;
    public AudioClip footStepsClip;
    public float soundRepeatTimer;

    // Update is called once per frame
    void Update()
    {
        CheckPlayerDistance();
        transform.LookAt(player);

        soundRepeatTimer += Time.deltaTime;
        if (inChaseRange && !inAttackRange)
        {
            anim.SetBool("inChaseRange", true);
            //PlayFootStepSound();
        }
        else
        {
            anim.SetBool("inChaseRange", false);
        }

        if (inAttackRange)
        {
            anim.SetBool("inAttackRange", true);
            anim.SetBool("inChaseRange", false);
            //AttackPlayer();
        }
        else
        {
            anim.SetBool("inAttackRange", false);
            anim.SetBool("inChaseRange", true);
            //ChasePlayer(); //It would seem this kind of movement does not work on a terrain I need to get familiar with terrains.

        }
        if (!inAttackRange && !inChaseRange)
        {
            anim.SetBool("inAttackRange", false);
            anim.SetBool("inChaseRange", false);
        }
        /*  if(inChaseRange && !inAttackRange)
          {
              anim.SetBool("inChaseRange", true);
          }
          else
          {
              anim.SetBool("inChaseRange", false);
          }*/
    }

    private void PlayFootStepSound()
    {
       
            footSteps.PlayOneShot(footStepsClip);
         
    }

    public void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        if (distanceToPlayer < attackRange)
        {
            inAttackRange = true;
        }
        else
        {
            inAttackRange = false;
        }
        if (distanceToPlayer < chaseRange)
        {
            inChaseRange = true;
        }
        else
        {
            inChaseRange = false;
        }
    }
    public void ChasePlayer()
    {
        Vector3 direction = player.transform.position;
        transform.forward = direction;
        transform.GetComponent<Rigidbody>().velocity = Vector3.MoveTowards(transform.position, player.transform.position, runningSpeed * Time.deltaTime);
    }
  
    private void OnDrawGizmos()
    {

        if (inChaseRange && !inAttackRange)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.blue;
        }
        Gizmos.DrawWireSphere(this.transform.position, chaseRange);

        if (inAttackRange)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireSphere(this.transform.position, attackRange);

    }
}
