using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IListener
{
    [SerializeField] private GameObject enemyPrefab;

    private IListener listener;

    public void RegisterListener(IListener _listener)
    {
        listener = _listener;
    }

    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    public void EnemyDefeated()
    {

    }

    public void Notify()
    {
        Debug.Log("Enemy has been defeated");
    }

    public void OnDestroy()
    {
        throw new System.NotImplementedException();
    }
}
