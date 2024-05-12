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
    [SerializeField] private const float STARTING_PLAYER_LIVES = 3;
    [SerializeField] private const float STARTING_HEALTH = 100;
    [SerializeField] private const float STARTING_SPEED = 5;
    [SerializeField] private const float STARTING_WEAPON_DAMAGE = 10;
    [SerializeField] private const float STARTING_WEAPON_RANGE = 4;
    [SerializeField] private const float STARTING_DEFLECT_RANGED_ATTACKS = 0f;

    [Header("References")]
    [SerializeField] private GameObject bonkStick;

    [Header("Current Game Properties")]
    [SerializeField] public float MaxHealth {  get; private set;}
    [SerializeField] public float PlayerLives { get; private set; }
    [SerializeField] public float CurrentHealth { get; private set; }
    [SerializeField] public float Speed { get; private set; }
    [SerializeField] public float WeaponDamage { get; private set; }
    [SerializeField] public float WeaponRange { get; private set; }
    [SerializeField] public float DeflectRangedAttacks { get; private set; }

    // References to player components
    private Player_Interactor player_Interactor;
    private Player_Inventory player_Inventory;
    private Player_Movement player_Movement;


    Component_Health health;
    [SerializeField] private Transform lastCheckpoint;

    private void Awake()
    {
        player_Interactor = gameObject.GetComponent<Player_Interactor>();
        player_Inventory = gameObject.GetComponent<Player_Inventory>();
        player_Movement = gameObject.GetComponent<Player_Movement>();
    }

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
        ModifyProperty(PlayerProperties.WeaponDamage, STARTING_WEAPON_DAMAGE);
        ModifyProperty(PlayerProperties.WeaponRange, STARTING_WEAPON_RANGE);
        ModifyProperty(PlayerProperties.DeflectRangedAttacks, STARTING_DEFLECT_RANGED_ATTACKS);
    }
    
    public void ModifyProperty(PlayerProperties _property, float _value)
    {
        switch (_property)
        {
            case PlayerProperties.Lives:
                PlayerLives = (int)_value;
                break;
            case PlayerProperties.Health:
                MaxHealth = _value;
                break;
            case PlayerProperties.Speed:
                Speed = _value;
                break;
            case PlayerProperties.WeaponDamage:
                bonkStick.GetComponent<BonkStick>().SetBonkStickDamage(_value);
                break;
            case PlayerProperties.WeaponRange:
                WeaponRange = _value;
                SetWeaponOrbitDistance(WeaponRange);
                break;
            case PlayerProperties.DeflectRangedAttacks:
                DeflectRangedAttacks = _value;
                bonkStick.GetComponent<BonkStick>().SetDeflectRangedAttacks(DeflectRangedAttacks > 0);
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
            Debug.Log("Killing player");
            KillPlayer();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Resetting player");
            ResetPlayer();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Modifying weapon range");
            ModifyProperty(PlayerProperties.WeaponRange, 10);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Resetting weapon range");
            ModifyProperty(PlayerProperties.WeaponRange, STARTING_WEAPON_RANGE);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Modifying weapon damage");
            ModifyProperty(PlayerProperties.WeaponDamage, 100);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Resetting weapon damage");
            ModifyProperty(PlayerProperties.WeaponDamage, STARTING_WEAPON_DAMAGE);
        }

        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Modifying weapon deflection");
            ModifyProperty(PlayerProperties.DeflectRangedAttacks, 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("Resetting weapon deflection");
            ModifyProperty(PlayerProperties.DeflectRangedAttacks, 0);
        }
    }

    public void SetWeaponOrbitDistance(float _distance)
    {
        bonkStick.transform.localPosition = new Vector3(0f, 0.25f, _distance);
    }
}
