using System.Collections.Generic;
using UnityEngine;

public enum RoomEventType
{
    None,
    Item,
    BossEnemy,
    Puzzle
}

public interface RoomEvent
{
    void OnTriggerEnter(Collider _player);
}
