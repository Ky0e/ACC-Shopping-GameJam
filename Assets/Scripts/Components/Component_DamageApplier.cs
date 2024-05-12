using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Component_DamageApplier : MonoBehaviour
{
    [field: SerializeField] public Collider AttackCollider;
    [SerializeField] private bool shouldDeactivate = true;

    public void ApplyDamage(Collider _target, int _ignoreLayer = -1, int _damage = 5, float _deactivateAfter = .2f)
    {
        //AttackCollider.gameObject.SetActive(true);

        //// Check for objects inside the collider and deal damage to them
        //Collider[] colliders = Physics.OverlapBox(AttackCollider.bounds.center, AttackCollider.bounds.extents, AttackCollider.transform.rotation);
        //// Debug.Log($"Number Of Attack Collision : {colliders.Length}");
        //foreach (Collider collider in colliders)
        //{
        //    if (collider == AttackCollider) { continue; }
        //    // Debug.Log($"Colliding With : {collider.name}");
        //    if (collider.gameObject.TryGetComponent(out IDamageable _damageableObject))
        //    {
        //        if (_ignoreLayer != -1)
        //        {
        //            if (collider.gameObject.layer == _ignoreLayer) { continue; }
        //        }
        //        _damageableObject.TakeDamage(_damage);
        //    }
        //}

        if (_target.gameObject.TryGetComponent(out IDamageable _damageableObject))
        {
            if (_ignoreLayer != -1)
            {
                if (_target.gameObject.layer == _ignoreLayer) { return; }
            }
            _damageableObject.TakeDamage(_damage);
        }

        // Deactivate the attack collider after a short delay
        if (shouldDeactivate) Invoke("DeactivateCollider", _deactivateAfter);
    }
    // Deactivate the attack collider
    private void DeactivateCollider()
    {
        AttackCollider.gameObject.SetActive(false);
    }
}



public interface IDamageable
{
    void TakeDamage(int damageAmount);
}