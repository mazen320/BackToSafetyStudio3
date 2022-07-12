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
    public Animator anim;
    public PlayerScript player;
    public Transform hand;
    Vector3 worldAimTarget;
    //public GameObject targetObj;

    [Header("Rifle shooting and ammunitition")]
    public int shotsLeft;
    private int magazineSize = 32;
    public int reservesLeft;
    public int maxReserves = 50;
    public int ammoRefil;//Amount of ammo you get when picking up an ammo box.
    public bool shooting;
    public bool shootingAndWalking;
    public bool aiming;
    public bool aimingAndShooting;
    public bool aimingAndWalking;
    public bool running;
    public bool runningAndAiming;
    public float reloadTime = 1.3f;
    private bool isReloading = false;
    private WeaponUIManager weaponUIManager;

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
    }
    public void ReadInputs()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            shooting = true;
        }
        else
        {
            shooting = false;
        }
        if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            shootingAndWalking = true;
        }
        else
        {
            shootingAndWalking = false;
        }
        if (Input.GetButton("Fire2"))
        {
            aiming = true;
        }
        else
        {
            aiming = false;
        }
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            aimingAndWalking = true;
        }
        else
        {
            aimingAndWalking = false;
        }
        if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            aimingAndShooting = true;
        }
        else
        {
            aimingAndShooting = false;
        }
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            running = true;
        }
        else
        {
            running = false;
        }
        
    }
    private void ShotHandler()
    {
        if (shooting)
        {

            anim.SetBool("Fire", true);
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", false);
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if (shootingAndWalking)
        {
            anim.SetBool("FireWalk", true);
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", false);
            player.playerSpeed = 0.5f;
        }
        else if (aiming)
        {
            anim.SetBool("IdleAim", true);
            anim.SetBool("FireWalk", false);
            anim.SetBool("Idle", false);

            worldAimTarget = cam.transform.position;
            worldAimTarget.y = player.transform.position.y;
            /*GameObject rightHand = GameObject.Find("mixamorig2:RightHand");

            rightHand.transform.forward = Vector3.Lerp(rightHand.transform.forward, cam.transform.forward, rotationSpeed * Time.deltaTime);*/
            player.transform.forward = Vector3.Lerp(player.transform.forward, cam.transform.forward, rotationSpeed * Time.deltaTime);
        }
        else if (aimingAndShooting)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            anim.SetBool("FireWalk", true);
            anim.SetBool("Walk", true);
            anim.SetBool("Reloading", false);
        }
        else if (aimingAndWalking)
        {
            anim.SetBool("RifleWalk", true);
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", false);
            anim.SetBool("FireWalk", false);
            anim.SetBool("Walk", false);
            anim.SetBool("Reloading", false);

            //player.transform.rotation = Quaternion.Lerp(player.transform.rotation, cam.transform.rotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("FireWalk", false);
            anim.SetBool("Fire", false);
            anim.SetBool("IdleAim", false);
            player.playerSpeed = 1.8f;
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
            player.playerSpeed = 0f;
            player.playerSprint = 0f;
            isReloading = true;
            Debug.Log("Reloading");

            anim.SetBool("Reloading", true);
            reload.PlayOneShot(reloadClip);//Sound Effect.

            yield return new WaitForSeconds(reloadTime);
            anim.SetBool("Reloading", false);

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
            player.playerSpeed = 1.9f;
            player.playerSprint = 3;
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
