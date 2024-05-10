using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BasicKaren : Enemy
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject pursePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float projectileForce;
    [SerializeField] float shootRange;
    GameObject target;
    bool isShooting;


    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
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
