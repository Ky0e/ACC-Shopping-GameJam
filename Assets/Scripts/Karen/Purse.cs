using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purse : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnCollisionEnter(Collision collision)
    {
        Component_Health health = collision.gameObject.GetComponentInParent<Component_Health>();
        if (health)
        {
            Debug.Log("Player takes " + damage + " damage");
            health.DecreaseHealth(damage);
        }
        Destroy(gameObject);
    }
}
