using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Room : MonoBehaviour, IListener
{
    [Header("Room Setup")]
    [SerializeField] private GameObject triggerVolume;
    [Header("Door Controlls")]
    [SerializeField, Tooltip("Should the doors lock on entry?")] protected bool lockDoorsOnEntry;
    [SerializeField, Tooltip("What item unlocks the door?")] protected CardSO itemRequiredToStartRoomEvent;
    [SerializeField, Tooltip("Doors to lock/unlock")] protected List<DoorController> doorsToControl;

    private List<GameObject> roomEnemiesToTrack = new List<GameObject>();

    protected void RegisterSpawn(GameObject _enemy)
    {
        roomEnemiesToTrack.Add(_enemy);
    }

    public void Notify(GameObject _messenger)
    {
        if(roomEnemiesToTrack.Contains(_messenger))
        {
            roomEnemiesToTrack.Remove(_messenger);
            if (roomEnemiesToTrack.Count == 0)
            {
                EndEvent();
            }
        }
        else
        {
            roomEnemiesToTrack.Add(_messenger);
        }
    }

    public void OnDestroy()
    {
        foreach (var enemy in roomEnemiesToTrack)
        {
            enemy.GetComponent<Enemy>().UnregisterListener(this);
        }
    }

    protected void LockRoom()
    {
        foreach (var door in doorsToControl)
        {
            door.CloseDoor();
            door.LockDoor();
        }
    }

    protected void UnlockRoom()
    {
        foreach (DoorController door in doorsToControl)
        {
            door.UnlockDoor();
            door.OpenDoor();
        }
    }

    protected abstract void EndEvent();
    
}
