using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] public int currentHealth;
    [SerializeField] private Transform head;
    private HealthBar healthBar;

    void Start() {
        StartCoroutine(DamagePatient());
        currentHealth = maxHealth;
    }

    // private void Update() {
    //     transform.position = head.position - transform.position
    // }

    public Rigidbody GetHandle() {
        return head.GetComponent<Rigidbody>();
    }

    public void Move(Vector3 here) {
        transform.position = here;
    }
    
    void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0) {
            // StopCoroutine(DamagePatient());
            Destroy(this.gameObject);
        }
    }

    IEnumerator DamagePatient() {
        while (true) {
            Debug.Log("Patient Hurt");
            TakeDamage(20);
            yield return new WaitForSeconds(1);
        }
    }

}
