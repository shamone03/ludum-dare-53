using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatientSpawner : MonoBehaviour {

    [SerializeField] private GameObject patient;
    [SerializeField] private GameObject currentPatient;
    
    private Vector3 randomMin = new Vector3(0, 0, 0);
    private Vector3 randomMax = new Vector3(0, 0, 0);
    private Vector3 randomPoint = new Vector3(0, 0, 0);
    private Collider randomCollider;

    private BoxCollider[] colliders;
    private void Start() {

        colliders = GetComponentsInChildren<BoxCollider>();
        randomCollider = colliders[0];
        currentPatient = Instantiate(patient);
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner() {
        while (true) {

            randomCollider = colliders[Random.Range(0, colliders.Length)];
            randomMax = randomCollider.bounds.max;
            randomMin = randomCollider.bounds.min;
            
            
            // if (currentPatient != null && currentPatient.GetComponent<Patient>().currentHealth <= 0) {
            //     Destroy(currentPatient);
            // }
            if (currentPatient == null) {
                Debug.Log("Patient Spawned");
                randomPoint.x = Random.Range(randomMin.x, randomMax.x);
                randomPoint.y = Random.Range(randomMin.y, randomMax.y);
                randomPoint.z = Random.Range(randomMin.z, randomMax.z);
                currentPatient = Instantiate(patient, randomPoint, Random.rotation);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
