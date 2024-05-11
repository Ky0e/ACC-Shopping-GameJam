
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IListenerTarget, IDestructible
{

    List<IListener> listeners = new List<IListener>();
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
        //spawnLoot();
    }
}
