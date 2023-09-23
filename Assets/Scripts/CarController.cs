using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    [SerializeField] private CarInput input = null;

    private void Awake() {
        input = new CarInput();
    }

    private void OnAccelerateStart(InputAction.CallbackContext value) {
        verticalInput = value.ReadValue<float>();
    }
    
    private void OnAccelerateStop(InputAction.CallbackContext value) {
        verticalInput = 0;
    }

    private void OnSteerStart(InputAction.CallbackContext value) {
        horizontalInput = value.ReadValue<float>();
    }
    
    private void OnSteerStop(InputAction.CallbackContext value) {
        horizontalInput = 0;
    }
    
    private void OnBrakeStart(InputAction.CallbackContext value) {
        isBraking = value.ReadValue<float>() > 0;
    }

    private void OnBrakeStop(InputAction.CallbackContext value) {
        isBraking = false;
    }

    private void OnFlipStart(InputAction.CallbackContext value) {
        flipCar = value.ReadValue<float>() > 0;
    }
    
    private void OnFlipStop(InputAction.CallbackContext value) {
        flipCar = false;
    }
    
    private void OnEnable() {
        input.Enable();
        input.Acceleration.Default.performed += OnAccelerateStart;
        input.Acceleration.Default.canceled += OnAccelerateStop;
        input.Steering.Default.performed += OnSteerStart;
        input.Steering.Default.canceled += OnSteerStop;
        input.Flip.Default.performed += OnFlipStart;
        input.Flip.Default.canceled += OnFlipStop;
        input.Brake.Default.performed += OnBrakeStart;
        input.Brake.Default.canceled += OnBrakeStop;
        
    }

    private void OnDisable() {
        input.Disable();
        input.Acceleration.Default.performed -= OnAccelerateStart;
        input.Acceleration.Default.canceled -= OnAccelerateStop;
        input.Steering.Default.performed -= OnSteerStart;
        input.Steering.Default.canceled -= OnSteerStop;
        input.Flip.Default.performed -= OnFlipStart;
        input.Flip.Default.canceled -= OnFlipStop;
        input.Brake.Default.performed -= OnBrakeStart;
        input.Brake.Default.canceled -= OnBrakeStop;
        
    }

    private void GetInput() {
        Debug.Log("accel: " + verticalInput);
        Debug.Log("steer: " + horizontalInput);
        Debug.Log("flip: " + flipCar);
        Debug.Log("brake: " + isBraking);
        // horizontalInput = Input.GetAxis("Horizontal");
        // verticalInput = Input.GetAxis("Fire1");
        // isBraking = Input.GetButton("Jump");
        // flipCar = Input.GetKeyDown(KeyCode.F);
    }

    private void Accelerate() {
        // Debug.Log("Accelerating");
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
        // Debug.Log("Braking");
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
            // Debug.Log("Steering Right");
        } else {
            // Debug.Log("Steering Left");
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

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        
        UIController.instance.SetSpeed(Mathf.RoundToInt(rb.velocity.magnitude));
    }
}
