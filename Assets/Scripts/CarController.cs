using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
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
    [SerializeField] private float motorForce = 1500;
    [SerializeField] private float brakeForce = 10000;
    [SerializeField] private float maxSteerAngle = 45;
    [SerializeField] private float maxSpeed = 60f;
    
    [Space]
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float rbSpeed;
    
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private float brakeInput;
    private bool isBraking;

    
    private void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBraking = Input.GetButton("Jump");
    }

    private void Accelerate() {
        Debug.Log("Accelerating");
        if (rb.velocity.magnitude > maxSpeed) {
            motorForce = 0;
        } else {
            motorForce = 1500;
        }
        
        rearLeftCollider.motorTorque = motorForce * verticalInput;
        rearRightCollider.motorTorque = motorForce * verticalInput;
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

    public void Update() {
        GetInput();
        UpdateWheelTransforms(frontLeftCollider, frontLeftTransform);
        UpdateWheelTransforms(frontRightCollider, frontRightTransform);
        UpdateWheelTransforms(rearLeftCollider, rearLeftTransform);
        UpdateWheelTransforms(rearRightCollider, rearRightTransform);
        Accelerate();
        Brake();
        Steer();
    }
}
