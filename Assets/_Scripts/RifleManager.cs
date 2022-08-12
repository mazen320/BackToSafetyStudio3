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


    [Header("Rifle shooting and ammunitition")]
    public int shotsLeft;
    private int magazineSize = 32;
    public int reservesLeft;
    public int maxReserves = 50;
    public int ammoRefil;//Amount of ammo you get when picking up an ammo box.
    public bool coroutineRunning;

    public float reloadTime = 1.3f;
    private bool isReloading = false;
    private WeaponUIManager weaponUIManager;

    [Header("Animation Control")]
    public Animator anim;
    public AnimationClip reloadAnimation;
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

    public PlayerAnimator playerAnimator;

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

        if (shotsLeft <= 0 && !coroutineRunning)
        {
            StartCoroutine(Reload());
            Debug.Log("Repeat bruhhhhhhh");
            return;
        }
        if (Input.GetKeyDown(KeyCode.R) && shotsLeft < magazineSize)
        {
            print(shotsLeft);
            StartCoroutine(Reload());
            return;
        }

        ShotHandler();

    }
    public void ReadInputs()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            playerAnimator.shouldShoot = true;
        }
        else
        {
            playerAnimator.shouldShoot = false;
        }
        if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            playerAnimator.shootingAndWalkingForwards = true;
        }
        else
        {
            playerAnimator.shootingAndWalkingForwards = false;
        }
        if (Input.GetButton("Fire2") && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            playerAnimator.aiming = true;
        }
        else
        {
            playerAnimator.aiming = false;
        }

        if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            playerAnimator.aimingAndShooting = true;
        }
        else
        {
            playerAnimator.aimingAndShooting = false;
        }
    }

    private void ReadMovementInputs()
    {
        if (Input.GetButton("Fire2") && Input.GetAxis("Vertical") > 0 /*|| Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)*/)
        {
            playerAnimator.aimingForwardStrafe = true;
        }
        else
        {
            playerAnimator.aimingForwardStrafe = false;
        }
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.S))
        {
            playerAnimator.aimingBackwardStrafe = true;
        }
        else
        {
            playerAnimator.aimingBackwardStrafe = false;
        }
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.A))
        {
            playerAnimator.aimingLeftStrafe = true;
        }
        else
        {
            playerAnimator.aimingLeftStrafe = false;
        }
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.D))
        {
            playerAnimator.aimingRightStrafe = true;
        }
        else
        {
            playerAnimator.aimingRightStrafe = false;
        }


    }

    private void ShotHandler()
    {
        if (playerAnimator.shouldShoot)
        {
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if (playerAnimator.shootingAndWalkingForwards)
        {
            playerScript.playerSpeed = 0.5f;
        }
        else if (playerAnimator.aiming)
        {
            worldAimTarget = cam.transform.position;
            worldAimTarget.y = playerScript.transform.position.y;
            /*GameObject rightHand = GameObject.Find("mixamorig2:RightHand");

            rightHand.transform.forward = Vector3.Lerp(rightHand.transform.forward, cam.transform.forward, rotationSpeed * Time.deltaTime);*/
            playerScript.transform.forward = Vector3.Lerp(playerScript.transform.forward, cam.transform.forward, rotationSpeed * Time.deltaTime);
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
        coroutineRunning = true;

        if (reservesLeft > 0)
        {
            playerScript.playerSpeed = 0f;
            playerScript.playerSprint = 0f;
            //isReloading = true;
            playerAnimator.isReloading = true;
            Debug.Log("Reloading");

            anim.SetBool("Reloading", true);
            /*  int reloadCount = 0;
              if (reloadCount < 1)
              {*/
            reload.PlayOneShot(reloadClip);//Sound Effect.
                                           //reloadCount++;
                                           //}

            //yield return new WaitForSeconds(reloadTime);
            yield return new WaitForSeconds(reloadAnimation.length / 2);
            playerAnimator.isReloading = false;
            //anim.SetBool("Reloading", false);
            //playerScript.ChangeAnimationState(reloading);

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
            //isReloading = false;
            coroutineRunning = false;

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
