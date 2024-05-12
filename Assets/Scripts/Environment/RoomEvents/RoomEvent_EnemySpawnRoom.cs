using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomEvent_EnemySpawnRoom : Room, ITriggerableObject
{
    [Header("Room Enemies")]
    [SerializeField] private List<EnemySpawner> enemySpawners;

    private bool triggered = false;


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

    public void Triggered(Collider collider)
    {
        // Fail fast conditions
        if (triggered) return;
        if (!collider.gameObject.CompareTag("Player")) return;

        // Does the room require an item to be in the player's inventory?
        if (itemRequiredToStartRoomEvent)
        {
            Player_Inventory playerInventory = collider.gameObject.GetComponent<Player_Inventory>();
            if (!playerInventory.HasItem(itemRequiredToStartRoomEvent)) return;
        }

        // Do the room events
        SpawnEnemies();
        if (lockDoorsOnEntry) LockRoom();

        // This room has been triggered
        triggered = true;
    }
}
