using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Patient : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float damagePerSecond = 10;
    [SerializeField] public float currentHealth;

    [SerializeField] private Transform head;
    private HealthBar healthBar;

    void Start() {
        healthBar = UIController.instance.healthSlider.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        StartCoroutine(DamagePatient());
    }

    // private void Update() {
    //     transform.position = head.position - transform.position
    // }


    public void Move(Vector3 here) {
        transform.position = here;
    }
    
    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (UIController.instance == null) {
            Debug.Log("instance null");
        }
        if (UIController.instance.healthSlider == null) {
            Debug.Log("health slider null");
        }
        
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0) {
            // StopCoroutine(DamagePatient());
            Destroy(this.gameObject);
        }

    }

    IEnumerator DamagePatient() {
        while (true) {
            Debug.Log("Patient Hurt");

            TakeDamage(damagePerSecond);

            yield return new WaitForSeconds(1);
        }
    }
}
