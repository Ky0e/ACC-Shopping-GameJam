using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomEvent_EnemySpawnRoom : Room, IRoomEvent
{
    [SerializeField] private bool lockDoors;
    [SerializeField] private CardSO item;
    [SerializeField] private List<DoorController> doors;
    [SerializeField] private List<EnemySpawner> enemySpawners;

    private bool triggered = false;

    public void OnTriggerEnter(Collider _player)
    {
        // Fail fast conditions
        if (triggered) return;
        if (!_player.gameObject.CompareTag("Player")) return;

        // Does the room require an item to be in the player's inventory?
        if (item)
        {
            Player_Inventory playerInventory = _player.gameObject.GetComponent<Player_Inventory>();
            if (!playerInventory.HasItem(item)) return;
        }
            
        // Do the room events
        SpawnEnemies();
        if (lockDoors) LockRoom();

        // This room has been triggered
        triggered = true;
    }

    private void LockRoom()
    {
        foreach (var door in doors)
        {
            door.CloseDoor();
            door.LockDoor();
        }
    }

    private void UnlockRoom()
    {
        foreach (DoorController door in doors)
        {
            door.UnlockDoor();
            door.OpenDoor();
        }
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

    
    protected override void EndEvent()
    {
        UnlockRoom();
    }
}