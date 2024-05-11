using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDestructible
{
    [SerializeField] private float maxHealth = 100;
    Component_Health health;

    void Start()
    {
        health = gameObject.GetComponent<Component_Health>();
        health.SetHealth(maxHealth);

        health.OnHealthChanged += CheckHealth;
    }    

    void CheckHealth(float _health)
    {
        if(_health <= 0) KillPlayer();
    }

    private void KillPlayer()
    {
        Debug.Log("PLAYER died!");
    }

    public void OnDestroy()
    {
        throw new System.NotImplementedException();
    }
}
