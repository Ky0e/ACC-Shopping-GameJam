using System.Collections.Generic;
using UnityEngine;

public class RoomEvent_EnemySpawnRoom : Room, IRoomEvent
{
    [SerializeField] private bool lockDoors;
    [SerializeField] private CardSO item;
    [SerializeField] private List<DoorController> doors;
    [SerializeField] private List<EnemySpawner> enemies;

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
        foreach (var door in doors)
        {
            door.OpenDoor();
            door.UnlockDoor();
        }
    }

    private void SpawnEnemies()
    {
        foreach (EnemySpawner enemy in enemies)
        {
            enemy.SpawnEnemy();
        }
    }

    private void EndEvent()
    {
        UnlockRoom();
    }
}
