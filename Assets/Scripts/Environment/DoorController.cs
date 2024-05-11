using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private bool isLocked = false;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private CardSO key;
    [SerializeField, Tooltip("Is the item removed on use??")] protected bool keyIsRemovedOnUse = true;





    public void OnCollisionEnter(Collision _player)
    {
        if(_player.gameObject.tag == "Player")
        {
            Player_Inventory player_Inventory = _player.gameObject.GetComponent<Player_Inventory>();
            if (isLocked && key)
            {
                if(player_Inventory.HasItem(key))
                {
                    UnlockDoor();
                    if (keyIsRemovedOnUse)
                    {
                        player_Inventory.RemoveItem(key);
                    }
                }
            }
            OpenDoor();
        }
    }

    public void LockDoor()
    {
        isLocked = true;
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

    public void OpenDoor()
    {
        if(!isOpen && !isLocked)
        {
            //TODO: Do door open in scene
            gameObject.SetActive(false);

            isOpen = true;
        }

    }

    public void CloseDoor()
    {
        if(isOpen)
        {
            //TODO: Do door close in scene
            gameObject.SetActive(true);

            isOpen = false;
        }
    }    
}
