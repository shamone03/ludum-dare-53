using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    [Header("Grounded")] 
    [SerializeField] private bool frontLeft;
    [SerializeField] private bool frontRight;
    [SerializeField] private bool rearLeft;
    [SerializeField] private bool rearRight;
    
    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider frontRightCollider;
    [SerializeField] private WheelCollider rearLeftCollider;
    [SerializeField] private WheelCollider rearRightCollider;

    [Header("Wheel Transforms")]
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform frontRightTransform;
    [SerializeField] private Transform rearLeftTransform;
    [SerializeField] private Transform rearRightTransform;

    [Header("Car values")] 
    [SerializeField] private float currentMotorForce;
    [SerializeField] private float motorForce = 1500;
    [SerializeField] private float brakeForce = 10000;
    [SerializeField] private float maxSteerAngle = 45;
    [SerializeField] private float maxSpeed = 60f;
    
    [Space]
    [SerializeField] private Rigidbody rb;
    
    [Header("Input")]
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private float brakeInput;
    [SerializeField] private bool isBraking;
    [SerializeField] private bool flipCar;

    private void Start() {
        Debug.Log(rb.centerOfMass.x + ", " + rb.centerOfMass.y + ", " + rb.centerOfMass.z);
    }

    private void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetButton("Jump");
        flipCar = Input.GetKeyDown(KeyCode.F);
    }

    private void Accelerate() {
        Debug.Log("Accelerating");
        if (rb.velocity.magnitude > maxSpeed) {
            currentMotorForce = 0;
        } else {
            currentMotorForce = motorForce;
        }
        
        rearLeftCollider.motorTorque = currentMotorForce * verticalInput;
        rearRightCollider.motorTorque = currentMotorForce * verticalInput;
        frontLeftCollider.motorTorque = currentMotorForce * verticalInput;
        frontRightCollider.motorTorque = currentMotorForce * verticalInput;
    }

    private void Brake() {
        Debug.Log("Braking");
        brakeInput = isBraking ? 1 : 0;
        if (isBraking) {
            rearLeftCollider.motorTorque = 0;
            rearRightCollider.motorTorque = 0;
        }
        frontRightCollider.brakeTorque = brakeForce * brakeInput;
        frontLeftCollider.brakeTorque = brakeForce * brakeInput;
        rearLeftCollider.brakeTorque = brakeForce * brakeInput;
        rearRightCollider.brakeTorque = brakeForce * brakeInput;
    }

    private void Steer() {
        if (horizontalInput > 0) {
            Debug.Log("Steering Right");
        } else {
            Debug.Log("Steering Left");
        }
        frontLeftCollider.steerAngle = maxSteerAngle * horizontalInput;
        frontRightCollider.steerAngle = maxSteerAngle * horizontalInput;
    }

    private void UpdateWheelTransforms(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void FlipCar() {
        if (flipCar) {
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void Update() {
        GetInput();
        FlipCar();
        UpdateWheelTransforms(frontLeftCollider, frontLeftTransform);
        UpdateWheelTransforms(frontRightCollider, frontRightTransform);
        UpdateWheelTransforms(rearLeftCollider, rearLeftTransform);
        UpdateWheelTransforms(rearRightCollider, rearRightTransform);
        frontLeft = frontLeftCollider.isGrounded;
        frontRight = frontRightCollider.isGrounded;
        rearLeft = rearLeftCollider.isGrounded;
        rearRight = rearRightCollider.isGrounded;
        Accelerate();
        Brake();
        Steer();
    }
}
