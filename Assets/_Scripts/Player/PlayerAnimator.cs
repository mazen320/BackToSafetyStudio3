using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    /// <summary>
    /// This class will control all animation transitions for the player and for the zombies. It will be the ONLY class that changes animations, so that no class overrides another.
    /// It is simply the bools required for a specific animation to play that will be set from their respective classes. This class checks if those bools are true or false...
    /// </summary>
    public Animator playerAnimator;
    public float transitionDuration;

    public Animator zombieAnimator;

    [Header("Player Boolean Checks")]
    public bool isWalking;
    //public bool isSprinting;

    public bool shouldShoot;
    public bool shootingAndWalkingForwards;

    public bool aiming;
    public bool aimingAndShooting;
    public bool aimingForwardStrafe;
    public bool aimingBackwardStrafe;
    public bool aimingLeftStrafe;
    public bool aimingRightStrafe;
    public bool isRunning;
    public bool runningAndAiming;

    public bool isReloading;

    [Header("Zombie Boolean Checks")]
    public bool inAttackRange;
    public bool inChaseRange;
    public bool zombieDied;

    [Header("Player Animation Names")]
    public string currentAnimState;

    const string idle = "Idle";
    const string walking = "Walk";
    const string running = "Running";

    const string shooting = "Shooting";
    const string shootWalkForward = "ShootWalkForward";
    const string idleAim = "IdleAim";
    const string aimIdleShoot = "AimIdleShoot";

    const string aimForwardWalk = "AimForwardWalk";
    const string aimBackwardWalk = "AimBackwardWalk";
    const string strafeLeft = "StrafeLeft";
    const string strafeRight = "StrafeRight";

    const string reloading = "Reloading";

    [Header("Zombie Animation Names")]
    const string attacking = "Attack";
    const string chasing = "Chasing";
    const string dying = "Falling Back Death";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayPlayerAnimations();
        PlayZombieAnimations();
    }
    private void PlayPlayerAnimations()
    {
         if (isReloading)
        {
            ChangePlayerAnimation(reloading);
        }
        else if (isWalking)
        {
            ChangePlayerAnimation(walking);
        }
        else if (isRunning)
        {
            print(isRunning);
            ChangePlayerAnimation(running);
        }
        else if (shouldShoot)
        {
            ChangePlayerAnimation(shooting);
        }
       
        else if (shootingAndWalkingForwards)
        {
            ChangePlayerAnimation(shootWalkForward);
        }
        else if (aiming)
        {
            ChangePlayerAnimation(idleAim);
        }
        else if (aimingAndShooting)
        {
            ChangePlayerAnimation(aimIdleShoot);
        }
        else if (aimingForwardStrafe)
        {
            ChangePlayerAnimation(aimForwardWalk);
        }
        else if (aimingBackwardStrafe)
        {
            ChangePlayerAnimation(aimBackwardWalk);
        }
        else if (aimingLeftStrafe)
        {
            ChangePlayerAnimation(strafeLeft);
        }
        else if (aimingRightStrafe)
        {
            ChangePlayerAnimation(strafeRight);
        }
        else
        {
            ChangePlayerAnimation(idle);
        }
    }
    private void PlayZombieAnimations()
    {
        if (inAttackRange)
        {
            ChangeZombieAnimation(attacking);
        }
        else if (inChaseRange)
        {
            ChangeZombieAnimation(chasing);
        }
        else if (zombieDied)
        {
            ChangeZombieAnimation(dying);
        }
    }
    private void ChangePlayerAnimation(string newState)
    {
        
        
        if (currentAnimState == newState)//Stops the current animation from interrupting itself.
        {
            return;
        }
        playerAnimator.CrossFade(newState, transitionDuration);
        //animator.Play(newState);
        currentAnimState = newState;
    }
    private void ChangeZombieAnimation(string newState)
    {


        if (currentAnimState == newState)//Stops the current animation from interrupting itself.
        {
            return;
        }
        zombieAnimator.CrossFade(newState, transitionDuration);
        //animator.Play(newState);
        currentAnimState = newState;
    }
}
