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
        Debug.Log("Enemy has been defeated");
        if (enemyObjects.Count == 0)
        {
            Debug.Log("All enemies have been defeated");
            EndEvent();
        }
    }

    public void OnDestroy()
    {
        throw new System.NotImplementedException();
    }

    protected abstract void EndEvent();
    
}
