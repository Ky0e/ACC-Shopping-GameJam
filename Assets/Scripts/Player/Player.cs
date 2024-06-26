using System;
using Unity.VisualScripting;
using UnityEngine;

public enum PlayerProperties
{
    None,
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
    [SerializeField] private const float STARTING_SPEED = 30;
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
    private Component_Health player_health;
    private string[] cardSos;


    Component_Health health;
    [SerializeField] private Transform lastCheckpoint;

    private void Awake()
    {
        player_Interactor = gameObject.GetComponent<Player_Interactor>();
        player_Inventory = gameObject.GetComponent<Player_Inventory>();
        player_Movement = gameObject.GetComponent<Player_Movement>();
        player_health = gameObject.GetComponent<Component_Health>();
        cardSos = Enum.GetNames(typeof(eCardType));
    }

    void Start()
    {
        ResetModifiers();

        player_health.SetHealth(MaxHealth);
        player_health.OnHealthChanged += CheckHealth;

        //player_Inventory.OnItemsListUpdated += ResetModifiers;
    }    

    void ResetPlayer()
    {
        if(lastCheckpoint != null)
        {
            ReturnPlayerToLastCheckpoint();
            player_health.SetHealth(MaxHealth);
        }
        else
        {
            Debug.Log("No checkpoint found, returning player to start position");
            gameObject.transform.position = Vector3.zero;
            player_health.SetHealth(MaxHealth);
        }
    }

    public void ResetModifiers()
    {
        ModifyProperty(PlayerProperties.Health, STARTING_HEALTH);
        ModifyProperty(PlayerProperties.Speed, STARTING_SPEED);
        ModifyProperty(PlayerProperties.WeaponDamage, STARTING_WEAPON_DAMAGE);
        ModifyProperty(PlayerProperties.WeaponRange, STARTING_WEAPON_RANGE);
        ModifyProperty(PlayerProperties.DeflectRangedAttacks, STARTING_DEFLECT_RANGED_ATTACKS);
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
                player_health.SetMaxHealth(MaxHealth);
                if(CurrentHealth > MaxHealth) player_health.SetHealth(MaxHealth);
                break;
            case PlayerProperties.Speed:
                Speed += _value;
                player_Movement.SetMovementValue(E_MovementValues.Speed_Walk, Speed);
                break;
            case PlayerProperties.WeaponDamage:
                WeaponDamage += _value;
                bonkStick.GetComponent<BonkStick>().SetBonkStickDamage(WeaponDamage);
                break;
            case PlayerProperties.WeaponRange:
                WeaponRange += _value;
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
        ModifyProperty(PlayerProperties.Lives, -1);
        if(PlayerLives > 0) ResetPlayer();
        else
        {
            Debug.Log("Game Over!");
            // Game over logic
        }
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
            Debug.Log("Adding Punch Card");
            player_Inventory.AddItem(player_Inventory.soHub.soCards[(int)eCardType.punchCard]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Removing Punch Card");
            player_Inventory.RemoveItem(player_Inventory.soHub.soCards[(int)eCardType.punchCard]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Adding Reverse Card");
            player_Inventory.AddItem(player_Inventory.soHub.soCards[(int)eCardType.reverseCard]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Removing Reverse Card");
            player_Inventory.RemoveItem(player_Inventory.soHub.soCards[(int)eCardType.reverseCard]);
        }

        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            player_Inventory.AddItem(player_Inventory.soHub.soCards[(int)eCardType.punchCard]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            player_Inventory.RemoveItem(player_Inventory.soHub.soCards[(int)eCardType.punchCard]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            player_Inventory.AddItem(player_Inventory.soHub.soCards[(int)eCardType.punchCard]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            player_Inventory.RemoveItem(player_Inventory.soHub.soCards[(int)eCardType.punchCard]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            player_Inventory.AddItem(player_Inventory.soHub.soCards[(int)eCardType.punchCard]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            player_Inventory.RemoveItem(player_Inventory.soHub.soCards[(int)eCardType.punchCard]);
        }
    }

    public void SetWeaponOrbitDistance(float _distance)
    {
        bonkStick.transform.localPosition = new Vector3(0f, 0.25f, _distance);
    }
}
