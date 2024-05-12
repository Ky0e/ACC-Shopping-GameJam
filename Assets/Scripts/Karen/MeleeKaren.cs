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
    private bool isAttacking;

    private float timeReminingUntilNextAttack;

    private void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        agent.stoppingDistance = distanceFromTarget;
        timeReminingUntilNextAttack = timeBetweenAttacks;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            KillEnemy();
        }

        if (isDead) return;

        timeReminingUntilNextAttack -= Time.deltaTime;
        if(timeReminingUntilNextAttack <= 0 )
        {
            isAttacking = true;
            timeReminingUntilNextAttack = timeBetweenAttacks;
        }

        agent.destination = target.transform.position;
        float distance = Vector3.Distance(agent.transform.position, target.transform.position);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (collision == null) return;
            if (!isAttacking) return;
            damageApplier.ApplyDamage(collision.collider, -1, 5);
            isAttacking = false;
        }
    }
}

