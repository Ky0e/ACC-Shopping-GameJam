using UnityEngine;

public class Player : MonoBehaviour, IDestructible
{
    [SerializeField] private float maxHealth = 100;
    Component_Health health;
    [SerializeField] private Transform lastCheckpoint;

    void Start()
    {
        health = gameObject.GetComponent<Component_Health>();
        health.SetHealth(maxHealth);

        health.OnHealthChanged += CheckHealth;
    }    

    void ResetPlayer()
    {
        if(lastCheckpoint != null)
        {
            ReturnPlayerToLastCheckpoint();
            health.SetHealth(maxHealth);
        }
        else
        {
            Debug.Log("No checkpoint found, returning player to start position");
            gameObject.transform.position = Vector3.zero;
            health.SetHealth(maxHealth);
        }
    }

    void CheckHealth(float _health)
    {
        if(_health <= 0) KillPlayer();
    }

    private void KillPlayer()
    {
        Debug.Log("PLAYER died!");
        ResetPlayer();
    }

    public void OnDestroy()
    {
        throw new System.NotImplementedException();
    }

    public void RegisterCheckpoint(Transform _checkpoint)
    {
        lastCheckpoint = _checkpoint;
    }

    public void ReturnPlayerToLastCheckpoint()
    {
        gameObject.transform.position = lastCheckpoint.position;
    }

    // DEBUG SECTION
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            KillPlayer();
        }
    }
}
