using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_Collision : MonoBehaviour
{
    [field: SerializeField] private float hitForce = 5f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.relativeVelocity.magnitude >= hitForce)
            {
                Debug.Log("HIT");
                Debug.Log(collision.relativeVelocity.magnitude);
                if (!collision.gameObject.TryGetComponent(out Rigidbody _rigidbodyComponent)) { return; }
                _rigidbodyComponent.AddExplosionForce(collision.relativeVelocity.magnitude * 50000, collision.contacts[0].point, 2);
            }
        }
    }
}
