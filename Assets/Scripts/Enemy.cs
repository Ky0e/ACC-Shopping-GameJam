
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IListenerTarget
{

    List<IListener> listeners = new List<IListener>();




    protected void KillEnemy()
    {
        Debug.Log("Enemy has been killed");
        NotifyListeners();
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
}
