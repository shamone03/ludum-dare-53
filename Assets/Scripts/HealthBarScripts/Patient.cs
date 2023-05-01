using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Patient : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] public int currentHealth;

    [SerializeField] private Transform head;
    public HealthBar healthBar;

    void Start() {
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
    
    public void TakeDamage(int damage) {
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

            TakeDamage(1);

            yield return new WaitForSeconds(1);
        }
    }
}
