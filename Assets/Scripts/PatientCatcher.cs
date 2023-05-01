using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientCatcher : MonoBehaviour {
    [SerializeField] private Transform entryPoint;
    [SerializeField] private GameObject currentPatient;
    private Transform nextPatient;
    [SerializeField] private bool caught;
    [SerializeField] private Transform arrow;
    private Collider collider;

    private void Start() {
        collider = GetComponent<Collider>();
        caught = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (!caught && currentPatient == null && other.gameObject.CompareTag("Patient")) {
            currentPatient = other.gameObject;
            Debug.Log("Patient in range");
            other.gameObject.GetComponentInParent<Patient>().Move(entryPoint.position);
        }
    }

    public void SetNextPatient(Transform patient) {
        nextPatient = patient;
    }

    private void Update() {
        caught = currentPatient != null && collider.bounds.Contains(currentPatient.transform.position);
        if (!caught) currentPatient = null;
        if (currentPatient == null) caught = false;

        if (!caught) {
            if (nextPatient != null) {
                arrow.gameObject.SetActive(true);
                arrow.LookAt(nextPatient);
                arrow.Rotate(new Vector3(1, 0, 0), 90f);
            }
        }
        else {
            arrow.gameObject.SetActive(false);
        }
    }
}
