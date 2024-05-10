using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEvent_BossRoom : MonoBehaviour, RoomEvent
{
    [SerializeField] private CardSO item;
    [SerializeField] private List<DoorController> doors;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform bossSpawnLocation;

    public void OnTriggerEnter(Collider _player)
    {
        if (_player.gameObject.CompareTag("Player"))
        {
            LockRoom();
            SpawnBoss();
        }
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
