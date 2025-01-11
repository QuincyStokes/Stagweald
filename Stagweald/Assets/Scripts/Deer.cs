using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Deer : MonoBehaviour
{
    //deer has health
    [Header("Health")]
    public float maxHealth;
    private float currentHealth;
    private bool alive;

    [Header("Movement")]
    public float moveSpeed;
    public float runDuration;

    [Header("Collider")]
    public Collider interactionCollider;
    public Rigidbody rb;

    [Header("Keybinds")]
    public KeyCode interactionKeyCode;
    private bool playerInRange;

    [Header("UI")]
    public GameObject interactionText;

    private DeerState deerState;
    private bool isCoroutineRunning;

    private enum DeerState
    {
        idle,
        walk,
        run,
        graze, 
        dead
    }
    void Start()
    {
        currentHealth = maxHealth;
        alive = true;
        interactionCollider.enabled = false;
        playerInRange = false;
        
    }

    void Update()
    {   
        //Vector3 vec = new Vector3(1f, 1f, 1f);
        
        //deer is alive
        if(!(deerState==DeerState.dead))
        {
            DeerStateHandler();
        }
        //deer is dead
        if((deerState==DeerState.dead) && playerInRange)
        {
            //this means we can interact with it
            if(Input.GetKeyDown(interactionKeyCode))
            {
                LootInteraction();
            }
        }
    }
        
    void DeerStateHandler()
    {
        if(deerState == DeerState.run)
        {

        }
    }


    public void TakeDamage(float damage, Vector3 position)
    {
        print("Deer took " + damage + " damage.");
        currentHealth -= damage;
        RunAway(position);
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


    void RunAway(Vector3 position)
    {
        
        Vector3 vec = transform.position - position;
        vec.y = 0;
        vec.Normalize();
        print(vec);
        print("AAAAAA");
        if(!isCoroutineRunning){
            StartCoroutine(Run(vec));
        }
        print("BBBBBB");
    }

    private IEnumerator Run(Vector3 dir)
    {
        isCoroutineRunning = true;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(dir);

        float timeElapsed = 0f;
        float rotationDuration = .2f;

        while (timeElapsed < rotationDuration)
        {
            timeElapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, timeElapsed/rotationDuration);
            yield return null;
        }

        transform.rotation = targetRotation;

        for(float i = 0; i < runDuration; i += Time.deltaTime)
        {
           rb.AddForce(dir * moveSpeed, ForceMode.Force);
           yield return null;
        }
        isCoroutineRunning = false;
    }


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
