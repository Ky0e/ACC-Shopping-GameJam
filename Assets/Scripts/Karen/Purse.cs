using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purse : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] bool friendlyFire = false;

    private const int purse_damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" || friendlyFire)
        {
            Component_DamageApplier da = gameObject.GetComponent<Component_DamageApplier>();
            if(da)
            {
                da.ApplyDamage(-1, purse_damage, 0.2f);
            }
            Component_Health health = collision.gameObject.GetComponentInParent<Component_Health>();
            if (health)
            {
                //Debug.Log("Player takes " + damage + " damage");
                //health.DecreaseHealth(damage);
            
            }
            Destroy(gameObject);
        }
    }
}
