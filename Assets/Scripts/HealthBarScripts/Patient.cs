using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patient : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] public int currentHealth;

    private HealthBar healthBar;

    void Start() {
        StartCoroutine(DamagePatient());
        currentHealth = maxHealth;
        healthBar = UIController.instance.healthSlider.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
    }

    // void Update()
    // {
    //     //Test Code
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         TakeDamage(10);
    //     }
    // }
    void TakeDamage(int damage) {
        currentHealth -= damage;
        // healthBar.SetHealth(currentHealth);
        // if (currentHealth <= 0) {
        //     // StopCoroutine(DamagePatient());
        //     Destroy(this.gameObject);
        // }
    }

    IEnumerator DamagePatient() {
        while (true) {
            Debug.Log("Patient Hurt");
            TakeDamage(10);
            yield return new WaitForSeconds(1);
        }
    }

}
