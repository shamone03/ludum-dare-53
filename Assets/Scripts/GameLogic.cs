using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
    // responsible for updating UI, keeping track of points
    [SerializeField] private float score;
    
    public void PatientDelivered(Patient patient) {
        score += patient.currentHealth;
        UIController.instance.score.text = score + "";
    }

    public void StartTimer() {
        
    }
}