
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IListenerTarget, IDestructible
{
    [field: SerializeField, BoxGroup("Attributes")] protected float maxHealth;

    protected List<IListener> listeners = new List<IListener>();
    protected bool isDead = false;
    protected Component_Health health;

    void Start()
    {
        health = gameObject.GetComponent<Component_Health>();
        SetStartingConditions();
    }
        protected void KillEnemy()
    {
        NotifyListeners();
        isDead = true;
        Destroy(this.gameObject);
    }

    public void NotifyListeners()
    {
        foreach (var listener in listeners)
        {
            listener.Notify(gameObject);
        }
    }

    public void RegisterListener(IListener _listener)
    {
        listeners.Add(_listener);
    }

    public void UnregisterListener(IListener _listener)
    {
        listeners.Remove(_listener);
    }

    public void OnDestroy()
    {
        //spawnLoot();
        gameObject.TryGetComponent<Component_LootSpawner>(out Component_LootSpawner _spawner);
        if(_spawner)
        {
            _spawner.GenerateLoot();
        }
    }

    protected void SetStartingConditions()
    {
        health.SetHealth(maxHealth);
        health.OnHealthChanged += CheckHealth;
    }

    void CheckHealth(float _health)
    {
        if (_health <= 0) KillEnemy();
    }
}
