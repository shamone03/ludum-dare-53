using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    // responsible for updating UI, keeping track of points
    [SerializeField] private float score;
    
    public void PatientDelivered(Patient patient) {
        
        score += patient.currentHealth;
        UIController.instance.timer.text = score + "";
    }
}