using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerKaren : Enemy
{
    [field: SerializeField, BoxGroup("Navigation Properties"), ReadOnly] private NavMeshAgent agent;
    [field: SerializeField, BoxGroup("Navigation Properties")] private float timeBetweenAttacks;
    [field: SerializeField, BoxGroup("Navigation Properties")] private float distanceFromTarget;
    [field: SerializeField, BoxGroup("Navigation Properties")] private GameObject target;
    [field: SerializeField, BoxGroup("Spawn Properties")] private float timeBetweenSpawns = 5f;
    [field: SerializeField, BoxGroup("Spawn Properties")] private List<GameObject> karensToSpawn;
    [field: SerializeField, BoxGroup("Spawn Properties")] private int maxKarensToSpawn;
    private bool isShooting;
    private GameObject playerOBJ;

    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        playerOBJ = GameObject.FindGameObjectWithTag("Player");
        // agent.stoppingDistance = distanceFromTarget;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            KillEnemy();
        }

        if (isDead) return;

        // Calculate the direction from the target to the enemy
        

        // Calculate the opposite point away from the target


        

        float distance = Vector3.Distance(agent.transform.position, playerOBJ.transform.position);
        if (distance <= 15f)
        {
            Vector3 directionToTarget = transform.position - playerOBJ.transform.position;
            Vector3 oppositePoint = transform.position + directionToTarget.normalized * distanceFromTarget;
            target.transform.position = oppositePoint;
            // Set the destination to the opposite point
            agent.SetDestination(target.transform.position);
        }
        else if (!isShooting) { StartCoroutine("PurseCoroutine"); }
    }
    IEnumerator PurseCoroutine()
    {
        isShooting = true;
        while (isShooting)
        {
            // Check if the player is within the shooting distance
            float distance = Vector3.Distance(agent.transform.position, playerOBJ.transform.position);
            if (distance <= 15f)
            {
                isShooting = false;
            }
            else
            {
                // Generate a random position around the SpawnerKaren
                Vector3 randomPosition = RandomNavSphere(transform.position, 10f);

                // Instantiate a random Karen enemy from the list at the random position
                GameObject randomKaren = karensToSpawn[Random.Range(0, karensToSpawn.Count)];
                GameObject _tempKaren = Instantiate(randomKaren, randomPosition, Quaternion.identity);
                foreach(var _listener in listeners) 
                { 
                    RegisterListener(_listener);
                    _listener.Notify(_tempKaren);
                }
            }

            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }

    // Function to generate random positions within a sphere around a center point
    private Vector3 RandomNavSphere(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas);
        return hit.position;
    }

}