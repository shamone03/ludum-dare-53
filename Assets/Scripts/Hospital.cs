using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Hospital : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Patient")) {
            Debug.Log("patient in hospital");
            UIController.instance.ScoreIncrement(other.gameObject.GetComponentInParent<Patient>().currentHealth);
            Destroy(other.gameObject.GetComponentInParent<Patient>().gameObject);
        }
    }
}
