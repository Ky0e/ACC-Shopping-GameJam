using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private bool isLocked = false;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private CardSO key;


    public void OnCollisionEnter(Collision _player)
    {
        if(_player.gameObject.tag == "Player")
        {
            if(isLocked && key)
            {
                if(_player.gameObject.GetComponent<Player_Inventory>().HasItem(key))
                {
                    UnlockDoor();
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
