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
        healthBar = UIController.instance.healthSlider.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(maxHealth);
    }

    // private void Update() {
    //     transform.position = head.position - transform.position
    // }

    public Rigidbody GetHandle() {
        return head.GetComponent<Rigidbody>();
    }

    public void Move(Vector3 here) {
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        // foreach (Rigidbody i in rbs) {
        //     i.isKinematic = true;
        // }
        // // transform.position = here + head.position;
        // Vector3 truePos = head.position - transform.position;
        // float newX;
        // float newZ;
        // if (here.x < head.position.x) {
        //     newX = here.x + truePos.x;
        // }
        //
        // Vector3 newPos = new Vector3(transform.position.x - (transform.position.x - head.position.x) - (head.position.x - here.x), here.y + Math.Abs(truePos.y), transform.position.z - (transform.position.z - head.position.z) - (head.position.z - here.z));
        // transform.position = newPos;
        transform.position = here;
        // Debug.Log(newPos);
        // float yDist = Math.Abs((head.position - transform.position).y);
        // transform.position = here + new Vector3(Math.Abs(truePos.x), Math.Abs(truePos.y), Math.Abs(truePos.z));
        // foreach (Rigidbody i in rbs) {
        //     i.isKinematic = false;
        // }
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
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0) {
            // StopCoroutine(DamagePatient());
            Destroy(this.gameObject);
        }
    }

    IEnumerator DamagePatient() {
        while (true) {
            Debug.Log("Patient Hurt");
            TakeDamage(10);
            yield return new WaitForSeconds(10);
        }
    }

}
