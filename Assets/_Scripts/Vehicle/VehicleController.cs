using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    [Header("Collider for Wheels")]
    public WheelCollider frontRightCollider;
    public WheelCollider frontLeftCollider;
    public WheelCollider BackRightCollider;
    public WheelCollider BackLeftCollider;

    [Header("Transform for Wheels")]
    public Transform frontRightTransform;
    public Transform frontLeftTransform;
    public Transform BackRightTransform;
    public Transform BackLeftTransform;
    public Transform vehicleDoor;

    [Header("Engine")]
    public float accelerationForce = 100f;
    public float breakingForce = 200f;
    private float currentBreakForce = 0f;
    public float currentAcceleration = 0f;

    [Header("Steering")]
    public float wheelsTorque = 20f;
    private float currentTurnAngle = 0f;

    [Header("Vehicle Other")]
    public PlayerScript player;
    private float radius = 5f;
    private bool isOpened = false;

    [Header("Things to Disabled")]
    public GameObject AimCam;
    public GameObject AimCanvas;
    public GameObject TPSCam;
    public GameObject TPSCanvas;
    public GameObject PlayerCharacter;



    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isOpened = true;
                radius = 5000f;
                //objective completed missions
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                player.transform.position = vehicleDoor.transform.position;
                isOpened = false;
                radius = 5f;
            }

        }
        if (isOpened == true)
        {
            TPSCam.SetActive(false);
            TPSCanvas.SetActive(false);
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
            PlayerCharacter.SetActive(false);

            MoveVehicle();
            VehicleSteering();
            BrakingSystem();
        }
        else if (isOpened == false)
        {
            TPSCam.SetActive(true);
            TPSCanvas.SetActive(true);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);
            PlayerCharacter.SetActive(true);
        }
    }

    void MoveVehicle()
    {
        frontRightCollider.motorTorque = currentAcceleration;
        frontLeftCollider.motorTorque = currentAcceleration;
        BackRightCollider.motorTorque = currentAcceleration;
        BackLeftCollider.motorTorque = currentAcceleration;         //all wheel drive cause of the mountainay terrain.

        currentAcceleration = accelerationForce * -Input.GetAxis("Vertical"); // vertical as forward
    }

    void VehicleSteering()
    {
        currentTurnAngle = wheelsTorque * Input.GetAxis("Horizontal");

        frontRightCollider.steerAngle = currentTurnAngle;
        frontLeftCollider.steerAngle = currentTurnAngle;

        //animate wheels

        SteeringtheWheels(frontRightCollider, frontRightTransform);
        SteeringtheWheels(frontLeftCollider, frontLeftTransform);
        SteeringtheWheels(BackRightCollider, BackRightTransform);
        SteeringtheWheels(BackLeftCollider, BackLeftTransform);
    }

    void SteeringtheWheels(WheelCollider wc, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;

        wc.GetWorldPose(out position, out rotation);

        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }

    void BrakingSystem()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakForce = breakingForce;
        }
        else
        {
            currentBreakForce = 0f;
        }
        frontRightCollider.brakeTorque = currentBreakForce;
        frontLeftCollider.brakeTorque = currentBreakForce;
        BackRightCollider.brakeTorque = currentBreakForce;
        BackLeftCollider.brakeTorque = currentBreakForce;
    }
}
