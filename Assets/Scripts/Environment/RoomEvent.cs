using System.Collections.Generic;
using UnityEngine;

public enum RoomEventType
{
    None,
    Item,
    BossEnemy,
    Puzzle
}

public class RoomEvent : MonoBehaviour
{
    [SerializeField] private RoomEventType roomEventType;
    [SerializeField] private CardSO item;
    [SerializeField] private List<DoorController> doors;

    private void OnTriggerEnter(Collider _player)
    {
        if (_player.gameObject.CompareTag("Player"))
        {
            switch(roomEventType)
            {
                case RoomEventType.Item:
                    if (!Player_Inventory.Instance.HasItem(item))
                    {
                        // Room event for item
                    }
                    break;
                case RoomEventType.BossEnemy:
                    // spawn enemy
                    foreach (var door in doors)
                    {
                        door.CloseDoor();
                        door.LockDoor();
                    }
                    break;
                case RoomEventType.Puzzle:
                    // start puzzle
                    break;
                default:
                    break;
            }
        }
    }
}
