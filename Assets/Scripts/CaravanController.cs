using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravanController : MonoBehaviour {
    [Header("Wheel Colliders")] 
    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider frontRightCollider;

    
    private void Start() {
        
    }

    private void Update() {
        frontLeftCollider.motorTorque = 0.0001f;
        frontRightCollider.motorTorque = 0.0001f;
    }
}