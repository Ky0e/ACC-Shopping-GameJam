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
    private bool isShooting;


    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        agent.stoppingDistance = distanceFromTarget;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            KillEnemy();
        }

        if (isDead) return;
        firePoint.LookAt(target.transform.position);
        agent.destination = target.transform.position;
        float distance = Vector3.Distance(agent.transform.position, target.transform.position);
        if (distance <= agent.stoppingDistance && !isShooting)
        {
            StartCoroutine("PurseCoroutine");
        }
    }
    IEnumerator PurseCoroutine()
    {
        isShooting = true;
        while (isShooting)
        {
            Fire();
            float distance = Vector3.Distance(agent.transform.position, target.transform.position);
            if (distance > agent.stoppingDistance)
            {
                isShooting = false;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void Fire()
    {
        Debug.Log("Fire");
        Rigidbody rb = Instantiate(pursePrefab,firePoint.position, firePoint.rotation).GetComponent<Rigidbody>();
        Vector3 force = rb.transform.forward * projectileForce;
        rb.AddForce(force, ForceMode.Impulse);
    }
}
