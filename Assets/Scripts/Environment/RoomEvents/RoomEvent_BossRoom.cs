using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvent_BossRoom : MonoBehaviour, IRoomEvent
{
    [SerializeField] private CardSO item;
    [SerializeField] private List<DoorController> doors;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform bossSpawnLocation;


    private bool triggered = false;

    public void OnTriggerEnter(Collider _player)
    {
        // Fail fast conditions
        if (triggered) return;
        if (!_player.gameObject.CompareTag("Player")) return;
            Player_Inventory playerInventory = _player.gameObject.GetComponent<Player_Inventory>();
        if (item) if(!playerInventory.HasItem(item)) return;

        LockRoom();
        SpawnBoss();
        
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

    private void SpawnBoss()
    {
        if(bossPrefab)
        {
            Instantiate(bossPrefab, bossSpawnLocation.position, bossSpawnLocation.rotation);
        }
    }
}
