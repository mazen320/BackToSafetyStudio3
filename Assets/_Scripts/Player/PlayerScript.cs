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

    //Animations when there is no aiming down sights.
    const string walk = "Walk";
    const string running = "Running";

    public float timeOfLastFunctionCall;
    public float interval = 5;



    


    public AudioSource footStep;
    public AudioClip footStepClip;

    public Transform grounded;
    bool isGrounded;
    public bool pickedAmmo;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    [Header("References")]
    public PlayerAnimator playerAnimator;
    public RifleManager rifleManager;
    public WeaponUIManager weaponUIManager;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {

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
    }

    void playerMovement()
    {
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetButton("Fire2"))
            {
                playerAnimator.isWalking = true;

            }
            else
            {
                playerAnimator.isWalking = false;
            }

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y; //we use the y angle for the camera so its left and right rather than up and down 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cC.Move(moveDirection.normalized * playerSpeed * Time.deltaTime);
        }
        else
        {
            playerAnimator.isWalking = false;
            //Debug.Log("You stopped");
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
    {/*
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {*/
        //playerAnimator.isRunning = true;
        float horizontal_axis = Input.GetAxisRaw("Horizontal");
        float vertical_axis = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;

        if (direction.magnitude >= 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetButton("Fire2"))
            {
                playerAnimator.isRunning = true;

            }
            else
            {
                playerAnimator.isRunning = false;
            }

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y; //we use the y angle for the camera so its left and right rather than up and down 
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            cC.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
        }
        //}
        else
        {
            playerAnimator.isRunning = false;
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
