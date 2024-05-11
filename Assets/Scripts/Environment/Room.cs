using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour, IListener
{

    private List<GameObject> enemyObjects = new List<GameObject>();

    protected void RegisterSpawn(GameObject _enemy)
    {
        enemyObjects.Add(_enemy);
    }

    public void Notify(GameObject _messenger)
    {
        enemyObjects.Remove(_messenger);
        if (enemyObjects.Count == 0)
        {
            EndEvent();
        }
    }

    public void OnDestroy()
    {
        foreach (var enemy in enemyObjects)
        {
            enemy.GetComponent<Enemy>().UnregisterListener(this);
        }
    }

    protected abstract void EndEvent();
    
}
