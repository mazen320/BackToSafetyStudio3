using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.8f;
    public float playerSprint = 3f;

    [Header("Player Camera")]
    public Transform playerCamera;

    [Header("Gravity")]
    public CharacterController cC;
    public float gravity = -9.81f; // gravity number

    [Header("Jumping & velocity")]
    public float turnTime = 0.1f;   //for smooth rotation
    float turnVelocity;
    public float jumpHeight = 1f;
    Vector3 velocity;

    [Header("Animation Control")]
    public Animator anim;
    public string currentAnimState;

    //Aiming and 'x' animations.
    const string idle = "Idle";
    /* const string idleAim = "IdleAim";
     const string aimForwardWalk = "AimForwardWalk";
     const string aimBackwardWalk = "AimBackwardWalk";
     const string strafeLeft = "StrafeLeft";
     const string strafeRight = "StrafeRight";*/

    //Animations when there is no aiming down sights.
    const string walk = "Walk";
    const string running = "Running";
    //const string crossPunch = "CrossPunch";





    public AudioSource footStep;
    public AudioClip footStepClip;

    public Transform grounded;
    bool isGrounded;
    public bool pickedAmmo;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("References")]
    public RifleManager rifleManager;
    public WeaponUIManager weaponUIManager;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.J))
         {
             ChangeAnimationState(walk);
         }
         if (Input.GetKeyDown(KeyCode.L))
         {
             anim.StopPlayback();
         }*/
        isGrounded = Physics.CheckSphere(grounded.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        cC.Move(velocity * Time.deltaTime);

        playerMovement();//Done.
        //Jump();
        Sprint();//Done.
        //ReadAnimationInputs();
    }
    public void ChangeAnimationState(string newState)
    {

        if (currentAnimState == newState)//Stops the current animation from interrupting itself.
        {
            return;
        }
        /* currentAnimState = newState;
         float animProgress = 0;//how close the current animation is to finishing.
         float animLength = 0;//total length of the current animation.
         RuntimeAnimatorController ac = anim.runtimeAnimatorController;
         Debug.Log("ac is " + ac);

         for (int i = 0; i < ac.animationClips.Length; i++)
         {
             if (ac.animationClips[i].name == currentAnimState)
             {
                 animLength = ac.animationClips[i].length;//animation length in seconds.
             }
         }
         animProgress += Time.deltaTime;
         if (animProgress >= animLength)
         {
             anim.Play(newState);
             animProgress = 0;
         }*/

        //anim.CrossFade(newState, 0.1f);
        //anim.Play(newState);
        //StartCoroutine(PlayAnimation(newState));
        anim.Play(newState);
        currentAnimState = newState;

    }
    IEnumerator PlayAnimation(string desiredAnimation)
    {
        anim.Play(desiredAnimation);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
    }
    /*public void ReadAnimationInputs()
    {
        if (Input.GetButtonDown("W"))
        {
            ChangeAnimationState(walk);
        }
        if (Input.GetButtonUp("W"))
        {
            ChangeAnimationState(idle);
        }
    }*/

    void playerMovement()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {

            /* anim.SetBool("Idle", false);
             anim.SetBool("Walk", true);
             anim.SetBool("Running", false);
             anim.SetBool("RifleWalk", false);
             anim.SetBool("IdleAim", false);*/

            ChangeAnimationState(walk);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y; //we use the y angle for the camera so its left and right rather than up and down 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            /* anim.SetBool("Walk", false);
             anim.SetBool("Idle", true);
             anim.SetBool("Running", false);*/
            ChangeAnimationState(idle);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetBool("Idle", false);
            anim.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.ResetTrigger("Jump");
        }
    }

    void Sprint()   //same thing from playermove just changed one variable to playersprint
    {
        if (Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && isGrounded)
        {
            float horizontal_axis = Input.GetAxisRaw("Horizontal");
            float vertical_axis = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                /*anim.SetBool("Walk", false);
                anim.SetBool("Running", true);*/
                ChangeAnimationState(running);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y; //we use the y angle for the camera so its left and right rather than up and down 
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                cC.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                /*anim.SetBool("Walk", true);
                anim.SetBool("Running", false);*/
                ChangeAnimationState(walk);
            }
        }
    }
    public void PlayFootStep()
    {
        footStep.PlayOneShot(footStepClip);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AmmoPickup")
        {
            rifleManager.reservesLeft += rifleManager.ammoRefil;
            weaponUIManager.UpdateReservesLeft(rifleManager.reservesLeft);
            rifleManager.reload.PlayOneShot(rifleManager.reloadClip);
            pickedAmmo = true;
            Debug.Log("Picked up ammo");
            Destroy(other.gameObject, 1f);
        }
    }

}
