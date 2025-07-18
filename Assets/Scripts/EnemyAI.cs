using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class EnemyAI : MonoBehaviour
{
    public Transform player; 
    public float speed = 3.0f;
    public float detectionRange = 10.0f; // This is the range that the enemy will start chasing the player
    public float attackRange = 2.0f;   // Range at which the enemy can attack the player
    public int damage = 10;            // Damage per attack
    public float attackInterval = 1.5f; // Time between attacks (seconds)
    private NavMeshAgent navMeshAgent; // Navmesh Agent for pathfinding
    private float nextAttackTime = 0f; // Timer to control attack intervals
    void Start()
    {
         // Initialize the NavMeshAgent component
        navMeshAgent = GetComponent<NavMeshAgent>();
    }



    void Update()
    {
        if (player != null && navMeshAgent.isOnNavMesh)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Move towards the player if not in attack range
                navMeshAgent.SetDestination(player.position);

                if (distanceToPlayer <= attackRange)
                {
                    // Stop moving when in attack range
                    navMeshAgent.ResetPath();

                    // Damage the player at intervals
                    if (Time.time >= nextAttackTime)
                    {
                        AttackPlayer();
                        nextAttackTime = Time.time + attackInterval;
                    }
                }
            }
            else
            {
                // Stop chasing when the player is out of detection range
                navMeshAgent.ResetPath();
            }
        }
    }

    // Attack the player and deal damage
    void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage, transform.position);
            Debug.Log("Enemy attacked the player for " + damage + " damage!");
        }
    }
}
