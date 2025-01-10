using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Deer : MonoBehaviour
{
    //deer has health
    [Header("Health")]
    public float maxHealth;
    private float currentHealth;
    private bool alive;

    [Header("Collider")]
    public Collider interactionCollider;

    [Header("Keybinds")]
    public KeyCode interactionKeyCode;
    private bool playerInRange;

    [Header("UI")]
    public GameObject interactionText;

    void Start()
    {
        currentHealth = maxHealth;
        alive = true;
        interactionCollider.enabled = false;
        playerInRange = false;
    }

    void Update()
    {
        //dangit im sure theres a way not to have it in update but oh well
        if(!alive && playerInRange)
        {
            //this means we can interact with it
            if(Input.GetKeyDown(interactionKeyCode))
            {
                LootInteraction();
            }
        }
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
        alive = false;
        //activate Interactible-ness
        ActivateInteractable();
        //when interacted with, play dissolve effect, despawn, give loot.
        //Destroy(gameObject);
    }



//Ragdoll  effect

        

    void ActivateInteractable()
    {
        //turn on collider
        interactionCollider.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            interactionText.SetActive(true);
            playerInRange = true;
            //ideally gets an outline or something to show it's interactable
            //"E" pops up somewhere on screen.
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            interactionText.gameObject.SetActive(false);
            playerInRange = false;
        }
    }

    void LootInteraction()
    {
        //give player items
        InventoryManager.Instance.AddItems(Random.Range(1,3), Random.Range(1,3));
        //play dissolve
        //delete
        interactionText.SetActive(false);
        Destroy(gameObject);
    }
}
