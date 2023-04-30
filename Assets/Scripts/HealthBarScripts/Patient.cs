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
        //healthBar = UIController.instance.healthSlider.GetComponent<HealthBar>();
        //healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        //nothing to do
    }
     void FixedUpdate()
    {

    }

    public void TakeDamage(int damage)
    {
        //call grunt noise
        currentHealth -= damage;
        //healthBar.SetHealth(currentHealth);

        if(currentHealth <= 0)
        {
            //end game / round / reset / lose / whatever we're doing for this
        }
    }
    IEnumerator DamagePatient() {
        while (true) {
            //Debug.Log("Patient Hurt");
            TakeDamage(1);
            yield return new WaitForSeconds(5);
        }
    }
    
}
