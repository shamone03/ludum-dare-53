using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravanController : MonoBehaviour {

    public float maxImpactForce = 10;
    public int damageAmount = 10;

    public AudioSource audioSource;
    public AudioClip grunt1;
    public AudioClip grunt2;
    public AudioClip grunt3;
    public AudioClip grunt4;
    public AudioClip grunt5;

    [Header("Wheel Colliders")]
    [SerializeField] private WheelCollider frontLeftCollider;
    [SerializeField] private WheelCollider frontRightCollider;

    [Header("Wheel Transforms")]
    [SerializeField] private Transform frontLeftTransform;
    [SerializeField] private Transform frontRightTransform;
    private void UpdateWheelTransforms(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void Update() {
        UpdateWheelTransforms(frontLeftCollider, frontLeftTransform);
        UpdateWheelTransforms(frontRightCollider, frontRightTransform);
        frontLeftCollider.motorTorque = 0.0001f;
        frontRightCollider.motorTorque = 0.0001f;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "head")
        {
            Debug.Log(collision.relativeVelocity.magnitude);
            float impactForce = collision.relativeVelocity.magnitude;
            if (impactForce > maxImpactForce)
            {
                int random = UnityEngine.Random.Range(1,6);
                if(random == 1){
                  audioSource.PlayOneShot(grunt1);
                }
                else if(random == 2){
                  audioSource.PlayOneShot(grunt2);
                }
                else if(random == 3){
                  audioSource.PlayOneShot(grunt3);
                }
                else if(random == 4){
                  audioSource.PlayOneShot(grunt4);
                }
                else if(random == 5){
                  audioSource.PlayOneShot(grunt5);
                }
                Debug.Log("OUCH");
                collision.gameObject.GetComponentInParent<Patient>().TakeDamage(damageAmount);
            }
        }
    }
}
