using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleManager : MonoBehaviour
{
    [Header("Rifle Initals")]
    public Camera cam;
    public int rotationSpeed;
    public float damage = 10f;
    public float bulletRange;
    public float fireCharge = 15f;
    private float nextTimeToShoot = 0;
    public PlayerScript playerScript;
    public Transform hand;
    Vector3 worldAimTarget;
    //public GameObject targetObj;

    [Header("Rifle shooting and ammunitition")]
    public int shotsLeft;
    private int magazineSize = 32;
    public int reservesLeft;
    public int maxReserves = 50;
    public int ammoRefil;//Amount of ammo you get when picking up an ammo box.
    public bool shouldShoot;
    public bool shootingAndWalkingForwards;

    public bool aiming;
    public bool aimingAndShooting;
    public bool aimingForwardStrafe;
    public bool aimingBackwardStrafe;
    public bool aimingLeftStrafe;
    public bool aimingRightStrafe;
    public bool running;
    public bool runningAndAiming;
    public float reloadTime = 1.3f;
    private bool isReloading = false;
    private WeaponUIManager weaponUIManager;

    [Header("Animation Control")]
    public Animator anim;

    const string idle = "Idle";
    const string shooting = "Shooting";
    const string shootWalkForward = "ShootWalkForward";
    const string idleAim = "IdleAim";
    const string aimIdleShoot = "AimIdleShoot";

    const string aimForwardWalk = "AimForwardWalk";
    const string aimBackwardWalk = "AimBackwardWalk";
    const string strafeLeft = "StrafeLeft";
    const string strafeRight = "StrafeRight";

    const string reloading = "Reloading";

    [Header("Rifle Effects")]
    public ParticleSystem muzzleFlash;
    public GameObject muzzle;

    public GameObject ConcreteEffect;

    public AudioSource shoot;
    public AudioClip shootClip;

    public AudioSource reload;
    public AudioClip reloadClip;


    private void Awake()
    {
        cam = Camera.main;
        transform.SetParent(hand);
        shotsLeft = magazineSize;
        reservesLeft = maxReserves;
        weaponUIManager = GameObject.Find("AmmoDisplay").GetComponent<WeaponUIManager>();
        muzzle = GameObject.FindGameObjectWithTag("Muzzle");
        muzzleFlash.gameObject.transform.position = muzzle.transform.position;
        rotationSpeed = 20;
    }
    void Update()
    {
        ReadInputs();
        ReadMovementInputs();
        if (isReloading)
            return;

        if (shotsLeft <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetKey(KeyCode.R) && shotsLeft < magazineSize)
        {
            StartCoroutine(Reload());
            return;
        }

        ShotHandler();
        MovementHandler();
    }
    public void ReadInputs()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            shouldShoot = true;
        }
        else
        {
            shouldShoot = false;
        }
        if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            shootingAndWalkingForwards = true;
        }
        else
        {
            shootingAndWalkingForwards = false;
        }
        if (Input.GetButton("Fire2"))
        {
            aiming = true;
        }
        else
        {
            aiming = false;
        }

        //ReadMovementInputs();
        
        if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            aimingAndShooting = true;
        }
        else
        {
            aimingAndShooting = false;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            running = true;
        }
        else
        {
            running = false;
        }

    }

    private void ReadMovementInputs()
    {
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) /*|| Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)*/)
        {
            aimingForwardStrafe = true;
        }
        else
        {
            aimingForwardStrafe = false;
        }
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.S))
        {
            aimingBackwardStrafe = true;
        }
        else
        {
            aimingBackwardStrafe = false;
        }
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.A))
        {
            aimingLeftStrafe = true;
        }
        else
        {
            aimingLeftStrafe = false;
        }
        if(Input.GetButton("Fire2") && Input.GetKey(KeyCode.D))
        {
            aimingRightStrafe = true;
        }
        else
        {
            aimingRightStrafe = false;
        }
       

    }

    private void ShotHandler()
    {
        if (shouldShoot)
        {

            /*anim.SetBool("Fire", true);
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", false);*/
            playerScript.ChangeAnimationState(shooting);
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if (shootingAndWalkingForwards)
        {
            /* anim.SetBool("FireWalk", true);
             anim.SetBool("Idle", false);
             anim.SetBool("IdleAim", false);*/
            playerScript.ChangeAnimationState(shootWalkForward);
            playerScript.playerSpeed = 0.5f;
        }
        else if (aiming)
        {
            /*anim.SetBool("IdleAim", true);
            anim.SetBool("FireWalk", false);
            anim.SetBool("Idle", false);*/
            playerScript.ChangeAnimationState(idleAim);

            worldAimTarget = cam.transform.position;
            worldAimTarget.y = playerScript.transform.position.y;
            /*GameObject rightHand = GameObject.Find("mixamorig2:RightHand");

            rightHand.transform.forward = Vector3.Lerp(rightHand.transform.forward, cam.transform.forward, rotationSpeed * Time.deltaTime);*/
            playerScript.transform.forward = Vector3.Lerp(playerScript.transform.forward, cam.transform.forward, rotationSpeed * Time.deltaTime);
        }
        else if (aimingAndShooting)
        {
            /* anim.SetBool("Idle", false);
             anim.SetBool("IdleAim", true);
             anim.SetBool("FireWalk", true);
             anim.SetBool("Walk", true);
             anim.SetBool("Reloading", false);*/
            playerScript.ChangeAnimationState(aimIdleShoot);
        }
       /* else if (aimingForwardStrafe)
        {
            anim.SetBool("RifleWalk", true);
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", false);
            anim.SetBool("FireWalk", false);
            anim.SetBool("Walk", false);
            anim.SetBool("Reloading", false);

            //player.transform.rotation = Quaternion.Lerp(player.transform.rotation, cam.transform.rotation, rotationSpeed * Time.deltaTime);
        }*/
        else
        {
            /*anim.SetBool("Idle", true);
            anim.SetBool("FireWalk", false);
            anim.SetBool("Fire", false);
            anim.SetBool("IdleAim", false);*/
            playerScript.ChangeAnimationState(idle);
            playerScript.playerSpeed = 1.8f;
        }
    }
    public void MovementHandler()
    {
        if(aimingForwardStrafe)
        {
            /* anim.SetBool("RifleWalk", true);
             anim.SetBool("Idle", false);
             anim.SetBool("IdleAim", false);
             anim.SetBool("FireWalk", false);
             anim.SetBool("Walk", false);
             anim.SetBool("Reloading", false);*/
            playerScript.ChangeAnimationState(aimForwardWalk);

            //player.transform.rotation = Quaternion.Lerp(player.transform.rotation, cam.transform.rotation, rotationSpeed * Time.deltaTime);
        }
        else if (aimingBackwardStrafe)
        {
            playerScript.ChangeAnimationState(aimBackwardWalk);
        }
        else if (aimingLeftStrafe)
        {
            /* anim.SetBool("AimLeftStrafe", true);
             anim.SetBool("RifleWalk", false);
             anim.SetBool("Idle", false);
             anim.SetBool("IdleAim", false);
             anim.SetBool("FireWalk", false);
             anim.SetBool("Walk", false);
             anim.SetBool("Reloading", false);*/
            playerScript.ChangeAnimationState(strafeLeft);
        }
        else if (aimingRightStrafe)
        {
            playerScript.ChangeAnimationState(strafeRight);
        }
    }
    private void Shoot()
    {

        if (shotsLeft > 0)
        {
            shotsLeft--;
            shoot.PlayOneShot(shootClip);
            weaponUIManager.UpdateShotsLeft(shotsLeft);//Displays current ammo in the magazine.

            muzzleFlash.Play();
            RaycastHit hitInfo;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, bulletRange))
            {
                Debug.Log(hitInfo.transform.name);
                ShootObject shootObject = hitInfo.transform.GetComponent<ShootObject>();

                if (shootObject != null)
                {
                    shootObject.ZombieTakeDamage(damage);
                    GameObject impactShot = Instantiate(ConcreteEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    Destroy(impactShot, 1f);
                }
            }
        }

    }
    IEnumerator Reload()
    {
        if (reservesLeft > 0)
        {
            playerScript.playerSpeed = 0f;
            playerScript.playerSprint = 0f;
            isReloading = true;
            Debug.Log("Reloading");

            anim.SetBool("Reloading", true);
            reload.PlayOneShot(reloadClip);//Sound Effect.

            yield return new WaitForSeconds(reloadTime);
            //anim.SetBool("Reloading", false);
            playerScript.ChangeAnimationState(reloading);

            int reloadAmmount = magazineSize - shotsLeft;
            if (reservesLeft > magazineSize)
            {
                shotsLeft += reloadAmmount;
            }
            else
            {
                shotsLeft += reservesLeft;
            }
            reservesLeft -= reloadAmmount;

            if (reservesLeft < 0)
            {
                reservesLeft = 0;

            }
            weaponUIManager.UpdateShotsLeft(shotsLeft);
            weaponUIManager.UpdateReservesLeft(reservesLeft);//Displays ammo left in the reserves.
            playerScript.playerSpeed = 1.9f;
            playerScript.playerSprint = 3;
            isReloading = false;
        }


    }

    

  /*  private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AmmoPickup")
        {
            reservesLeft += ammoRefil;
            weaponUIManager.UpdateReservesLeft(reservesLeft);
            Debug.Log("Picked up ammo");
            Destroy(other.gameObject, 1f);
        }
    }*/

}
