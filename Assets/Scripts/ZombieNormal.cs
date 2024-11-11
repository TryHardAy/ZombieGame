using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ZombieNormal : MonoBehaviour, IEnemies
{
    public int SpawnPropability = 10;

    [SerializeField] private float health = 100;
    [SerializeField] private float exp = 5;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float wanderRadius = 10.0f; // Radius within which the zombie will wander
    [SerializeField] private float followDistance = 20.0f; // Distance beyond which the zombie will stop following the player
    [SerializeField] private float detectionRadius = 5.0f; // Distance within which the zombie detects the player and starts following
    [SerializeField] private float biteDistance = 1.5f; // Distance within which the zombie starts biting the player
    [SerializeField] private float alarmRadius = 15.0f; // Radius within which other zombies get alarmed
    [SerializeField] private float attackDistance = 5.0f; // Distance within which the zombie can attack the player
    [SerializeField] private float attackDamage = 2.0f; // Damage dealt by the zombie
    [SerializeField] private Transform ItemSpawnPoint;
    [SerializeField] private GameObject[] Items = new GameObject[5];

    private Transform player;
    private Player playerScript;
    private NavMeshAgent navMeshAgent;
    private Vector3 spawnPoint;
    private bool isFollowing;
    private Animator animator;
    private bool isDead = false;

    private static readonly int Wandering = Animator.StringToHash("Wandering");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Death = Animator.StringToHash("Death");

    public void GetDamaged(float value, OnDeath onDeath)
    {
        if (isDead) return;

        health -= value;

        if (IsDead())
        {
            isDead = true;
            navMeshAgent.isStopped = true; // Stop the zombie's movement
            navMeshAgent.enabled = false; // Disable the NavMeshAgent
            animator.SetTrigger(Death); // Trigger the Death animation
            StartCoroutine(Die(onDeath)); // Start the death coroutine
            OnDeath();
        }
    }

    bool IsDead()
    {
        return health <= 0;
    }

    private void OnDeath()
    {
        if (UnityEngine.Random.Range(1, 101) > SpawnPropability)
            return;

        int index = UnityEngine.Random.Range(0, Items.Length);

        GameObject chosenOne = Items[index];

        Instantiate(chosenOne, ItemSpawnPoint.position, ItemSpawnPoint.rotation);
    }

    IEnumerator Die(OnDeath onDeath)
    {
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        onDeath(exp);
        yield return new WaitForSeconds(30.0f); // Wait for the death animation to finish
        Destroy(gameObject);
    }

    private void ImportStats()
    {
        attackDamage = GameManager.ZombieDmg;
        speed = GameManager.ZombieSpeed;
    }

    void Start()
    {
        ImportStats();
        animator = GetComponent<Animator>(); // Initialize the Animator component
        GameObject playerObject = GameObject.Find("Gczlowiek2");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerScript = playerObject.GetComponent<Player>();
        }
        else
        {
            Debug.LogError("Player object with name 'Gczlowiek2' not found.");
        }

        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = speed / 2;  // Wandering speed
            spawnPoint = transform.position;

        }
        else
        {
            Debug.LogError("NavMeshAgent component is missing from the zombie.");
        }

        isFollowing = true;
        animator.SetTrigger(Wandering);
    }

    void Update()
    {
        if (isDead) return;

        if (player != null && navMeshAgent != null && navMeshAgent.isOnNavMesh)
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);

            if (distanceToPlayer <= biteDistance)
            {
                animator.SetTrigger(Attack);
            }

            if (isFollowing && distanceToPlayer > biteDistance)
            {
                navMeshAgent.speed = speed;  // Following speed
                navMeshAgent.SetDestination(player.position);
            }
            else if (!isFollowing)
            {
                navMeshAgent.speed = speed / 2;  // Wandering speed
            }
        }
    }

    public void OnAttackFinished()
    {
        DealDamageInRange();
    }

    void DealDamageInRange()
    {
        if (player != null && Vector3.Distance(player.position, transform.position) <= attackDistance)
        {
            Debug.Log("Dealt damage to the player.");
            playerScript.TakeDamage(attackDamage);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
