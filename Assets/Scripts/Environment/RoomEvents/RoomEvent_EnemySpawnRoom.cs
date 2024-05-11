using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomEvent_EnemySpawnRoom : Room, IRoomEvent
{
    [Header("Room Enemies")]
    [SerializeField] private List<EnemySpawner> enemySpawners;

    private bool triggered = false;

    // What to do when the player enters the room trigger area
    public void OnTriggerEnter(Collider _player)
    {
        // Fail fast conditions
        if (triggered) return;
        if (!_player.gameObject.CompareTag("Player")) return;

        // Does the room require an item to be in the player's inventory?
        if (keyToUnlockDoors)
        {
            Player_Inventory playerInventory = _player.gameObject.GetComponent<Player_Inventory>();
            if (!playerInventory.HasItem(keyToUnlockDoors)) return;
        }
            
        // Do the room events
        SpawnEnemies();
        if (lockDoorsOnEntry) LockRoom();

        // This room has been triggered
        triggered = true;
    }

    private void SpawnEnemies()
    {
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            GameObject enemy = enemySpawner.SpawnEnemy();
            enemy.GetComponent<Enemy>().RegisterListener(this);
            RegisterSpawn(enemy);
        }
    }

    // What to do when the event ends
    protected override void EndEvent()
    {
        UnlockRoom();
    }
}
