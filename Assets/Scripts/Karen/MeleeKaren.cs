using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MeleeKaren : Enemy
{
    [field: SerializeField, BoxGroup("Navigation Properties"), ReadOnly] private NavMeshAgent agent;
    [field: SerializeField, BoxGroup("Navigation Properties")] private float timeBetweenAttacks;
    [field: SerializeField, BoxGroup("Navigation Properties")] private float distanceFromTarget;
    [field: SerializeField, BoxGroup("Melee Properties")] private float damageAmount = 5f;
    [field: SerializeField, BoxGroup("Melee Properties")] private Component_DamageApplier damageApplier;
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
            damageApplier.ApplyDamage(-1, (int)damageAmount);
            float distance = Vector3.Distance(agent.transform.position, target.transform.position);
            if (distance > agent.stoppingDistance)
            {
                isShooting = false;
            }
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
    }
}

