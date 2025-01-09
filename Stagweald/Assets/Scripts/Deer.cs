using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : MonoBehaviour
{
    //deer has health
    [Header("Health")]
    public float maxHealth;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        print("Deer took " + damage + " damage.");
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }


    //making this a function for now to implement animation + loot spawning later.
    void Die()
    {
        
        Destroy(gameObject);
    }
}
