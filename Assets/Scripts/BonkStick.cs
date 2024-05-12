using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonkStick : MonoBehaviour
{
    [SerializeField] bool friendlyFire = false;

    float damage;

    public void SetBonkStickDamage(float _damage)
    {
        damage = _damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Component_DamageApplier da = gameObject.GetComponent<Component_DamageApplier>();
            if (da)
            {
                da.ApplyDamage(collision.collider, -1, Mathf.RoundToInt(damage), 0.2f);
            }
        }
    }
}
