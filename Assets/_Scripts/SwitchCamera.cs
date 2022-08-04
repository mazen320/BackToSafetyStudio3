using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject aimCam;
    public GameObject aimCanvas;
    public GameObject thirdPersonCanvas;
    public GameObject thirdPersonCam;

    //public Animator anim;

    
    

    void Update()
    {
       /* if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("RifleWalk", true);
            anim.SetBool("Idle", false);
            anim.SetBool("IdleAim", false);
            anim.SetBool("Walk", false);

            thirdPersonCam.SetActive(false);
            thirdPersonCanvas.SetActive(false);
            aimCam.SetActive(true);
            aimCanvas.SetActive(true);
        }
        else if (Input.GetButton("Fire2"))
        {
            anim.SetBool("IdleAim", true);
            anim.SetBool("Idle", false);
            anim.SetBool("RifleWalk", false);
            anim.SetBool("Walk", false);

            thirdPersonCam.SetActive(false);
            thirdPersonCanvas.SetActive(false);
            aimCam.SetActive(true);
            aimCanvas.SetActive(true);
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("IdleAim", false);
            anim.SetBool("RifleWalk", false);

            thirdPersonCam.SetActive(true);
            thirdPersonCanvas.SetActive(true);
            aimCam.SetActive(false);
            aimCanvas.SetActive(false);
        }

*/
        if (Input.GetButton("Fire2"))//Just need to check if player is aiming then change camera, all animation transitions have been done by other scripts.
        {
            thirdPersonCam.SetActive(false);
            thirdPersonCanvas.SetActive(false);
            aimCam.SetActive(true);
            aimCanvas.SetActive(true);
        }
        else
        {
            thirdPersonCam.SetActive(true);
            thirdPersonCanvas.SetActive(true);
            aimCam.SetActive(false);
            aimCanvas.SetActive(false);
        }
    }

}
