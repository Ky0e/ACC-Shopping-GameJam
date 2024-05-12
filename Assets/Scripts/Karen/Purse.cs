using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purse : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] bool friendlyFire = false;
    [SerializeField] float timeToLive = 2f;

    float timeRemaming;

    private void Start()
    {
        timeRemaming = timeToLive;
    }

    private void Update()
    {
        timeRemaming -= Time.deltaTime;
        if (timeRemaming <= 0) Destroy(gameObject); 


    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" || friendlyFire)
        {
            Component_DamageApplier da = gameObject.GetComponent<Component_DamageApplier>();
            if(da)
            {
                da.ApplyDamage(collision.collider, -1, Mathf.RoundToInt(damage), 0.2f);
            }

            Destroy(gameObject);
        }
    }
}
