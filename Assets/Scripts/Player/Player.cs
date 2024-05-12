using UnityEngine;

public enum PlayerProperties
{
    Lives,
    Health,
    Speed,
    WeaponDamage,
    WeaponRange,
    DeflectRangedAttacks
}

public class Player : MonoBehaviour, IDestructible
{
    [Header("Player Properties")]
    [SerializeField] private const string PLAYER_NAME = "Player";
    [SerializeField] private const int STARTING_PLAYER_LIVES = 3;
    [SerializeField] private const float STARTING_HEALTH = 100;
    [SerializeField] private const float STARTING_SPEED = 5;
    [SerializeField] private const float STARTING_WEAPON_DAMAGE = 10;
    [SerializeField] private const float STARTING_WEAPON_RANGE = 5;
    [SerializeField] private const bool STARTING_DEFLECT_RANGED_ATTACKS = false;

    [Header("Current Game Properties")]
    [SerializeField] public float MaxHealth {  get; private set;}
    [SerializeField] public int PlayerLives { get; private set; }
    [SerializeField] public float CurrentHealth { get; private set; }
    [SerializeField] public float Speed { get; private set; }
    [SerializeField] public float WeaponDamage { get; private set; }
    [SerializeField] public float WeaponRange { get; private set; }
    [SerializeField] public bool DeflectRangedAttacks { get; private set; }


    Component_Health health;
    [SerializeField] private Transform lastCheckpoint;

    void Start()
    {
        ResetModifiers();

        health = gameObject.GetComponent<Component_Health>();
        health.SetHealth(MaxHealth);

        health.OnHealthChanged += CheckHealth;
    }    

    void ResetPlayer()
    {
        if(lastCheckpoint != null)
        {
            ReturnPlayerToLastCheckpoint();
            health.SetHealth(MaxHealth);
        }
        else
        {
            Debug.Log("No checkpoint found, returning player to start position");
            gameObject.transform.position = Vector3.zero;
            health.SetHealth(MaxHealth);
        }
    }

    public void ResetModifiers()
    {
        MaxHealth = STARTING_HEALTH;
        Speed = STARTING_SPEED;
        WeaponDamage = STARTING_WEAPON_DAMAGE;
        WeaponRange = STARTING_WEAPON_RANGE;
        DeflectRangedAttacks = STARTING_DEFLECT_RANGED_ATTACKS;
    }
    
    public void ModifyProperty(PlayerProperties _property, float _value)
    {
        switch (_property)
        {
            case PlayerProperties.Lives:
                PlayerLives += (int)_value;
                break;
            case PlayerProperties.Health:
                MaxHealth += _value;
                break;
            case PlayerProperties.Speed:
                Speed += _value;
                break;
            case PlayerProperties.WeaponDamage:
                WeaponDamage += _value;
                break;
            case PlayerProperties.WeaponRange:
                WeaponRange += _value;
                break;
            case PlayerProperties.DeflectRangedAttacks:
                if(_value > 0 ) DeflectRangedAttacks = true;
                else DeflectRangedAttacks = false;
                break;
            default:
                break;
        }
    }

    void CheckHealth(float _health)
    {
        CurrentHealth = _health;
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
