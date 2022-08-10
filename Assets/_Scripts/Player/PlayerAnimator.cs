using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    /// <summary>
    /// This class will controll all animation transitions for the player and for the zombies. It will be the ONLY class that changes animations, so that no class overrides another.
    /// It is simply the bools required for a specific animation to play that will be set from their respective classes. This class checks if those bools are true or false...
    /// </summary>
    public Animator animator;

    [Header("Boolean Checks")]
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

    [Header("Animation Names")]
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayPlayerAnimations();
    }
    private void PlayPlayerAnimations()
    {
         if (isReloading)
        {
            ChangeAnimationState(reloading);
        }
        else if (isWalking)
        {
            ChangeAnimationState(walking);
        }
        else if (isRunning)
        {
            print(isRunning);
            ChangeAnimationState(running);
        }
        else if (shouldShoot)
        {
            ChangeAnimationState(shooting);
        }
       
        else if (shootingAndWalkingForwards)
        {
            ChangeAnimationState(shootWalkForward);
        }
        else if (aiming)
        {
            ChangeAnimationState(idleAim);
        }
        else if (aimingAndShooting)
        {
            ChangeAnimationState(aimIdleShoot);
        }
        else if (aimingForwardStrafe)
        {
            ChangeAnimationState(aimForwardWalk);
        }
        else if (aimingBackwardStrafe)
        {
            ChangeAnimationState(aimBackwardWalk);
        }
        else if (aimingLeftStrafe)
        {
            ChangeAnimationState(strafeLeft);
        }
        else if (aimingRightStrafe)
        {
            ChangeAnimationState(strafeRight);
        }
        else
        {
            ChangeAnimationState(idle);
        }
    }
    private void PlayZombieAnimations()
    {

    }
    private void ChangeAnimationState(string newState)
    {
        bool sameState = currentAnimState == newState;
        print("same state?: " + sameState);
        if (sameState)//Stops the current animation from interrupting itself.
        {
            return;
        }
       
        animator.Play(newState);
        currentAnimState = newState;
    }
}
