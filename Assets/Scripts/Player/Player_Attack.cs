using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    [SerializeField, BoxGroup("Components")] private InputReader manager_Input;
    [SerializeField] private Transform guillotinePivot;
    [SerializeField] private float swingSpeed = 90f; // Degrees per second

    private Vector3 rotationAxis;

    private void Update()
    {
        // Get input for rotation
        rotationAxis = manager_Input.RotationAxis;

        // If there's input, start the swing
        //if (rotationAxis.magnitude > 0.01f)
        //{
        //    float angle = (90 * rotationAxis.x) + transform.localRotation.y;
        //    Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        //    guillotinePivot.rotation = Quaternion.RotateTowards(guillotinePivot.rotation, targetRotation, swingSpeed * Time.deltaTime);
        //}
    }


}

/*    [field: SerializeField] private InputReader manager_Input;
    [field: SerializeField] private Component_DamageApplier damageApplier;
    private bool isAttacking = false;

    private void Awake()
    {
        damageApplier.AttackCollider.enabled = false;
        damageApplier.AttackCollider.isTrigger = true;
        
    }

    private void Update()
    {
        if(!isAttacking && manager_Input.Attack)
        {
            isAttacking = true;
            damageApplier.ApplyDamage();
        }
    }*/

/*
    [SerializeField, BoxGroup("Components")] private InputReader manager_Input;
    [SerializeField] private Transform guillotinePivot;
    [SerializeField] private float swingSpeed = 90f; // Degrees per second

    private Vector3 rotationAxis;

    private void Update()
    {
        // Get input for rotation
        rotationAxis = manager_Input.RotationAxis;

        // If there's input, start the swing
        if (rotationAxis.magnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rotationAxis.y, rotationAxis.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            guillotinePivot.rotation = Quaternion.RotateTowards(guillotinePivot.rotation, targetRotation, swingSpeed * Time.deltaTime);
        }
    }
 */