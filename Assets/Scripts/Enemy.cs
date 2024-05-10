
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IListenerTarget
{

    List<IListener> listeners = new List<IListener>();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Debug.Log("Enemy has been killed");
        NotifyListeners();
        Destroy(this.gameObject);
    }

    public void NotifyListeners()
    {
        foreach (var listener in listeners)
        {
            listener.Notify();
        }
    }

    public void RegisterListener(IListener _listener)
    {
        listeners.Add(_listener);
    }
}
