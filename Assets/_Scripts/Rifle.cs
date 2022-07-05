using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Initals")]
    public Camera cam;
    public float damage = 10f;
    public float bulletRange = 100f;
    public float fireCharge = 15f;
    private float nextTimeToShoot = 0;
    public Animator anim;
    public PlayerScript player;
    public Transform hand;

    [Header("Rifle shooting and ammunitition")]
    public int currentAmmo;
    private int maxAmmo = 32;
    public int magazinesLeft = 10;
    public bool shooting;
    public bool shootingAndWalking;
    public bool aiming;
    public bool aimingAndShooting;
    public float reloadTime = 1.3f;
    private bool isReloading = false;
    private WeaponUIManager weaponUIManager;

    [Header("Rifle Effects")]
    public ParticleSystem muzzleFlash;
    public GameObject ConcreteEffect;

    public AudioSource shoot;
    public AudioClip shootClip;
    
    public AudioSource reload;
    public AudioClip reloadClip;


    private void Awake()
    {
        transform.SetParent(hand);
        currentAmmo = maxAmmo;
        weaponUIManager = GameObject.Find("AmmoDisplay").GetComponent<WeaponUIManager>();
    }

    void Update()
    {
        ReadInputs();
        if (isReloading)
            return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetKey(KeyCode.R) && currentAmmo != maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        ShotHandler();
    }
    public void ReadInputs()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            shooting = true;
        }
        else
        {
            shooting = false;
        }
        if(Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
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
        if(Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            aimingAndShooting = true;
        }
        else
        {
            aimingAndShooting = false;
        }
    }

    private void ShotHandler()
    {
        if (shooting)
        {

            anim.SetBool("Fire", true);
            anim.SetBool("Idle", false);

            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if (shootingAndWalking)
        {
            anim.SetBool("FireWalk", true);
            anim.SetBool("Idle", false);
            player.playerSpeed = 0.5f;
        }
        else if (aimingAndShooting)
        {
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", true);
            anim.SetBool("FireWalk", true);
            anim.SetBool("Walk", true);
            anim.SetBool("Reloading", false);
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("FireWalk", false);
            anim.SetBool("Fire", false);
            player.playerSpeed = 1.8f;
        }
    }

    private void Shoot()
    {
        //check the mag
        if (magazinesLeft == 0)
        {
            //show ammo in text
            return;
        }
        currentAmmo--;
        shoot.PlayOneShot(shootClip);
        weaponUIManager.UpdateShotsLeft(currentAmmo);//Displays current ammo.
        if (currentAmmo == 0)
        {
            magazinesLeft--;
        }
        //update ui when implemented from here

        //muzzleFlash.Play();
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, bulletRange))
        {
            Debug.Log("You shot at " + hitInfo.transform.name);
            ShootObject shootObject = hitInfo.transform.GetComponent<ShootObject>();

            if (shootObject != null)
            {
                shootObject.ZombieTakeDamage(damage);
                GameObject impactShot = Instantiate(ConcreteEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(impactShot, 1f);
            }
        }
    }
    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        isReloading = true;
        Debug.Log("Reloading");

        anim.SetBool("Reloading", true);
        reload.PlayOneShot(reloadClip);//Sound Effect.

        yield return new WaitForSeconds(reloadTime);
        anim.SetBool("Reloading", false);

        currentAmmo = maxAmmo;
        weaponUIManager.UpdateShotsLeft(currentAmmo);//Displays current ammo.
        player.playerSpeed = 1.9f;
        player.playerSprint = 3;
        isReloading = false;
    }
}
