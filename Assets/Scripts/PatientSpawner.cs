using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatientSpawner : MonoBehaviour {

    [SerializeField] private GameObject patient;
    [SerializeField] private float xMin, xMax, yMin, yMax, zMin, zMax;
    private Vector3 randomPoint = new Vector3(0, 0, 0);
    private GameObject currentPatient;
    private void Start() {
        currentPatient = Instantiate(patient);
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner() {
        while (true) {
            if (currentPatient != null && currentPatient.GetComponent<Patient>().currentHealth <= 0) {
                Destroy(currentPatient);
            }
            if (currentPatient == null) {
                Debug.Log("Patient Spawned");
                randomPoint.x = Random.Range(xMin, xMax);
                randomPoint.y = Random.Range(yMin, yMax);
                randomPoint.z = Random.Range(zMin, zMax);
                currentPatient = Instantiate(patient, randomPoint, Random.rotation);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
