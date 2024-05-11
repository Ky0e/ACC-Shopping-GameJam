using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkStick : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] bool friendlyFire = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Component_DamageApplier da = gameObject.GetComponent<Component_DamageApplier>();
            if (da)
            {
                da.ApplyDamage(-1, Mathf.RoundToInt(damage), 0.2f);
            }
        }
    }
}
