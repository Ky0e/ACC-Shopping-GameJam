using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;

    public GameObject SpawnEnemy()
    {
        return Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    public void EnemyDefeated()
    {

    }
}
