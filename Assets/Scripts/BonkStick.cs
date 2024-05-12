using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BonkStick : MonoBehaviour
{
    [SerializeField] bool friendlyFire = false;

    float damage;
    bool deflectRangedAttacks = false;

    public void SetBonkStickDamage(float _damage)
    {
        damage = _damage;
    }

    public void SetDeflectRangedAttacks(bool _deflectRangedAttacks)
    {
        deflectRangedAttacks = _deflectRangedAttacks;
        if(deflectRangedAttacks)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("BonkStick");
        }
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
