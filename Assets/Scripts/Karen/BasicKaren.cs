using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BasicKaren : Enemy
{
    [field: SerializeField, BoxGroup("Navigation Properties"), ReadOnly] private NavMeshAgent agent;
    [field: SerializeField, BoxGroup("Navigation Properties")] private float timeBetweenAttacks;
    [field: SerializeField, BoxGroup("Navigation Properties")] private float distanceFromTarget;
    [field: SerializeField, BoxGroup("Ranged Attack Properties")] private GameObject pursePrefab;
    [field: SerializeField, BoxGroup("Ranged Attack Properties")] private Transform firePoint;
    [field: SerializeField, BoxGroup("Ranged Attack Properties")] private float projectileForce;
    [field: SerializeField, BoxGroup("Ranged Attack Properties")] private float shootRange;
    private GameObject target;

        float shotTimer;

    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        agent.stoppingDistance = distanceFromTarget;
        shotTimer = timeBetweenAttacks;
    }

    private void Update()
    {
        if (isDead) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            KillEnemy();
        }

        shotTimer -= Time.deltaTime;
        if(shotTimer <= 0 && Vector3.Distance(agent.transform.position, target.transform.position) <= agent.stoppingDistance)
        { 
            Fire();
            shotTimer = timeBetweenAttacks;
        }

        firePoint.LookAt(target.transform.position);
        agent.destination = target.transform.position;
    }

    private void Fire()
    {
        Rigidbody rb = Instantiate(pursePrefab,firePoint.position, firePoint.rotation).GetComponent<Rigidbody>();
        Vector3 force = rb.transform.forward * projectileForce;
        rb.AddForce(force, ForceMode.Impulse);
    }
}
