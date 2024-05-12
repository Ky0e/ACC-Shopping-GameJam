using UnityEngine;

public class Checkpoint : MonoBehaviour, ITriggerable
{
    public void Triggered(Collider _collider)
    {
        Debug.Log("Checkpoint triggered!");
        if(_collider.gameObject.TryGetComponent(out Player player))
        {
            player.RegisterCheckpoint(transform);
        }
    }

    void Start()
    {
        GetComponent<Renderer>().enabled = false;
    }
}

