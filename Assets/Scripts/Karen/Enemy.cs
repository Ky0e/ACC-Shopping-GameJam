
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IListenerTarget, IDestructible, IDamageable
{
    protected List<IListener> listeners = new List<IListener>();
    protected bool isDead = false;

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
        Debug.Log("Destroyed");
        //spawnLoot();
        gameObject.TryGetComponent<Component_LootSpawner>(out Component_LootSpawner _spawner);
        if(_spawner)
        {
            Debug.Log("Karen has a loot spawner");
            _spawner.GenerateLoot();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("" + damageAmount + " damage dealt to ENEMY");
    }
}
