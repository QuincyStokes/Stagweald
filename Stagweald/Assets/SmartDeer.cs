using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SmartDeer : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Collider interactionCollider;
    public GameObject interactionText;

    [Header("Idle Settings")]
    public float wanderRadius = 20f;      // How far the deer can roam from its origin or a chosen center point
    public float wanderInterval = 10f;     // Time (seconds) between picking new wander points

    [Header("Flee Settings")]
    public float playerDetectionRange = 15f;
    public float fleeDistance = 30f;      // How far the deer tries to get away
    public float fleeSpeed;
    public float idleSpeed;

    [Header("Health")]
    public float maxHealth;
    private float currentHealth;
    private bool alive;

    [Header("Keybinds")]
    public KeyCode interactionKeyCode;
    private bool playerInRange;

    private DeerState deerState;
    private enum DeerState{
        fleeing,
        idle,
        dead
    }

    private Transform player;
    private float wanderTimer;

    private void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        // Initialize wander
        wanderTimer = wanderInterval;
        agent.updateRotation = false;
        currentHealth = maxHealth;
        interactionCollider.enabled = false;
        alive = true;
        playerInRange = false;
    }

    private void Update()
    {
        // Check if player is nearby
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        RotateTowardsMovementDirection();
        DeerStateHandler();
        if (distanceToPlayer <= playerDetectionRange && alive)
        {
            FleeFromPlayer();
        }
        else if (alive)
        {
            Idle();
        }
        else //must be dead
        {   

        }

        if(!alive && playerInRange)
        {
            //this means we can interact with it
            if(Input.GetKeyDown(interactionKeyCode))
            {
                LootInteraction();
            }
        }
    }

    private void DeerStateHandler()
    {
        if(deerState == DeerState.idle)
        {
            agent.speed = idleSpeed;
        }
        else if (deerState == DeerState.fleeing)
        {
            agent.speed = fleeSpeed;
        }
        else if (deerState == DeerState.dead)
        {

        }
    }

    private void Idle()
    {
        deerState = DeerState.idle;
        wanderTimer += Time.deltaTime;
        if (wanderTimer >= wanderInterval)
        {
            Vector3 newDestination = RandomWanderDestination();
            agent.SetDestination(newDestination);
            wanderTimer = 0f;
        }
    }

    private Vector3 RandomWanderDestination()
    {
        // Random point in sphere, scaled by wanderRadius
        Vector3 randomDir = Random.insideUnitSphere * wanderRadius;
        randomDir += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDir, out hit, wanderRadius, NavMesh.AllAreas);

        return hit.position;
    }

    private void FleeFromPlayer()
    {
        if (player == null) return;
        deerState = DeerState.fleeing;
        // direction away from the player
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        // find a point away from the player (fleeDistance away)
        Vector3 fleePos = transform.position + fleeDirection * fleeDistance;

        NavMeshHit hit;
        NavMesh.SamplePosition(fleePos, out hit, fleeDistance, NavMesh.AllAreas);

        agent.SetDestination(hit.position);
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

    public void TakeDamage(float damage, Vector3 position)
    {
        if(alive)
        {
            print("Deer took " + damage + " damage.");
            currentHealth -= damage;
            FleeFromPlayer();
            if(currentHealth <= 0)
            {
                Die();
            }
        }  
    }

    void Die()
    {
        alive = false;
        deerState = DeerState.dead;
        //activate Interactible-ness
        ActivateInteractable();
        //when interacted with, play dissolve effect, despawn, give loot.
        //Destroy(gameObject);
        agent.ResetPath();
        agent.enabled = false;
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

    private void RotateTowardsMovementDirection()
{
    // If the agent has some velocity, face that direction
    Vector3 direction = agent.desiredVelocity;
    direction.y = 0f; // Keep the deer upright
    
    // Only rotate if there's actual movement
    if (direction.sqrMagnitude > 0.001f)
    {
        // Smoothly rotate towards the desired direction
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * 5f
        );
    }
}
}
