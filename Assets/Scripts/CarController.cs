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

    private float horizontalInput;
    private float verticalInput;
    private float brakeInput;

    [Header("Car values")] 
    [SerializeField] private float motorForce = 1500;
    [SerializeField] private float brakeForce = 10000;
    [SerializeField] private float maxSteerAngle = 45;
    
    private void GetInput() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        brakeInput = verticalInput < 0 ? verticalInput * -1 : 0;
    }

    private void Accelerate() {
        Debug.Log("Accelerating");
        rearLeftCollider.motorTorque = motorForce * verticalInput;
        rearRightCollider.motorTorque = motorForce * verticalInput;
    }

    private void Brake() {
        Debug.Log("Braking");
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

    public void Update() {
        GetInput();
        Accelerate();
        Brake();
        Steer();
    }
}
