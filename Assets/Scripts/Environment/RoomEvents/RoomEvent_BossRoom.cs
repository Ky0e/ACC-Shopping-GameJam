using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class RoomEvent_BossRoom : Room, ITriggerableObject
{
    [Header("Boss Controlls")]
    [SerializeField, Tooltip("Add multiple spawners for phases")] private List<EnemySpawner> bossSpawners;

    private int numberOfPhases;
    private int currentPhase = 0;
    private bool triggered = false;

    public void Start()
    {
        numberOfPhases = bossSpawners.Count;
    }

    public void Triggered(Collider _player)
    {
        // Fail fast conditions
        if (triggered) return;
        if (!_player.gameObject.CompareTag("Player")) return;

        // Does the room require an item to be in the player's inventory?
        if (itemRequiredToStartRoomEvent)
        {
            Player_Inventory playerInventory = _player.gameObject.GetComponent<Player_Inventory>();
            if (!playerInventory.HasItem(itemRequiredToStartRoomEvent)) return;
        }

        // Do the room events
        LockRoom();
        SpawnBoss();
        
        // This room has been triggered
        triggered = true;
    }

    // spawn boss

    private void SpawnBoss()
    {
        // play boss cutscene
        // play boss music
        GameObject enemy = bossSpawners[currentPhase].SpawnEnemy();
        enemy.GetComponent<Enemy>().RegisterListener(this);
        RegisterSpawn(enemy);
    }

    protected override void EndEvent()
    {
        StepPhase();
    }

    private void StepPhase()
    {
        currentPhase++;
        if(currentPhase < numberOfPhases)
        {
            SpawnBoss();
        }
        else
        {
            UnlockRoom();
        }
    }
}

