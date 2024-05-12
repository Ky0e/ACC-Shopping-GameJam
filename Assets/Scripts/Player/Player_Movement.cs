using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

public class Player_Movement : AnimatorBrain
{
    [field: SerializeField, BoxGroup("Movement Variables")] private float walkSpeed = 5;
    [field: SerializeField, BoxGroup("Movement Variables")] private float sprintSpeed = 7;
    [field: SerializeField, BoxGroup("Movement Variables"), Range(0.01f, 1f), Space(12f)] private float movementThreshold = .01f;

    [field: SerializeField, BoxGroup("Movement Smoothing Variables"), Range(0f, 5f)] private float dampSpeedUp = .4f;
    [field: SerializeField, BoxGroup("Movement Smoothing Variables"), Range(0f, 5f)] private float dampSpeedDown = .4f;

    [field: SerializeField, BoxGroup("Rotation Variables")] private float rotationAmount = 2f;
    [field: SerializeField, BoxGroup("Rotation Variables"), Range(.01f, 20000f)] private float rotationSpeed = 2f;


    [Tooltip("How High The Player Remain Above The Ground")]
    [field: SerializeField, BoxGroup("Upright Variables")] private float rideHeight = 1;
    [Tooltip("Strength Of The Spring To Keep The Player Above The Ground")]
    [field: SerializeField, BoxGroup("Upright Variables")] private float rideSpringStrength = 2000;
    [Tooltip("Dampning Of The Spring To Provide Gradual Resistance")]
    [field: SerializeField, BoxGroup("Upright Variables")] private float rideSpringDamp = 45f;


    [Tooltip("Strength Of The Spring To Keep The Player Upright")]
    [field: SerializeField, BoxGroup("Upright Variables")] private float uprightSpringStrength = 2000f;
    [Tooltip("Dampning Of The Spring To Provide Gradual Resistance")]
    [field: SerializeField, BoxGroup("Upright Variables")] private float uprightSpringDamp = 45f;


    [field: SerializeField, BoxGroup("Components")] private InputReader manager_Input;
    [field: SerializeField, BoxGroup("Components")] private GameObject character_Camera;
    [field: SerializeField, BoxGroup("Components")] private Rigidbody character_Rigidbody;
    [field: SerializeField, BoxGroup("Components")] private Animator character_Animator;
    [field: SerializeField, BoxGroup("Components")] public GameObject Character_Visuals;
    //[field: SerializeField, BoxGroup("Components")] private CameraFollowObject character_FollowObject;

    public Rigidbody Character_Rigidbody { get { return character_Rigidbody; } }

    // Hidden Public Variables
    [field: SerializeField, HideInInspector] public bool CanMove = true;
    [field: SerializeField, ReadOnly, Foldout("Debug Variables")] public bool IsFacingForward = true;
    [field: SerializeField, ReadOnly, Foldout("Debug Variables")] public bool IsFacingRight = false;
    [field: SerializeField, ReadOnly, Foldout("Debug Variables")] public bool IsFacingLeft = false;
    [field: SerializeField, ReadOnly, Foldout("Debug Variables")] public bool IsTurning = false;
    [field: SerializeField, ReadOnly, Foldout("Debug Variables")] private List<float> defaultMovementValues;
    [field: SerializeField, ReadOnly, Foldout("Debug Variables")] private List<float> currentMovementValues;

    // Private Variables

    // Lists


    // Vectors
    private Vector2 movementAxis;
    private Vector3 direction_Forward;
    private Vector3 direction_Downward;
    private Vector3 direction_Right;
    private Vector3 currentVelocity = Vector3.zero;

    private const int UPPERBODY = 1;
    private const int LOWERBODY = 0;


    public static Player_Movement Instance;

    private void Awake()
    {
        if (Instance != null) { Destroy(Instance); }
        else { Instance = this; }

        if (character_Rigidbody == null) { character_Rigidbody = GetComponent<Rigidbody>(); }
        if (character_Animator == null) { character_Animator = GetComponentInChildren<Animator>(); }

        if ((character_Camera == null || manager_Input == null || Character_Visuals == null))
        {
            Debug.LogError("Character Visuals Or Camera, Or Input Is Equal To Null. Please Add Component Refrences");
        }

        direction_Downward = Vector3.down;
        direction_Forward = Vector3.forward;
        direction_Right = Vector3.right;
        SetupMovementValues();
        //InitializeAnimatorBrain(character_Animator.layerCount,
        //                        E_Animations.Idle, character_Animator,
        //                        DefaultAnimation);
    }

    private void Update()
    {
        Vector2 movement = manager_Input.RotationAxis;

        // Get input for rotation from right stick
        float rotateHorizontal = manager_Input.RotationAxis.x;
        float rotateVertical = manager_Input.RotationAxis.y;

        // Create movement and rotation vectors
        Vector3 rotation = new Vector3(rotateHorizontal, 0.0f, rotateVertical);

        // Rotate the character
        if (rotation != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(rotation, Vector3.up);
            Character_Visuals.transform.rotation = Quaternion.RotateTowards(Character_Visuals.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        //CheckTopAnimation();

        //if (character_Animator.layerCount > 1) CheckBottomAnimation();
    }
    private void FixedUpdate()
    {
        if (!CanMove) { return; }
        UpdateInputVariables();

        OnMove();
        ApplyHover();
        //UpdateUprightForce();

    }

    private void UpdateInputVariables()
    {
        movementAxis = manager_Input.MovementAxis;

        if (!IsFacingForward && movementAxis.y >= 0) { character_Rigidbody.AddForce(-direction_Forward * 250); }
        else if (IsFacingForward && movementAxis.y < 0) { character_Rigidbody.AddForce(direction_Forward * 250); }

        if (!IsFacingRight && movementAxis.x >= 0) { character_Rigidbody.AddForce(-direction_Right * 250); }
        else if (IsFacingRight && movementAxis.x < 0) { character_Rigidbody.AddForce(direction_Right * 250); }

        IsFacingForward = movementAxis.y >= 0;
        IsFacingRight = movementAxis.x >= 0;
    }

    #region Movement
    private void OnMove()
    {
        //if (movementAxis.magnitude > .1) { Play(E_Animations.Sprint, LOWERBODY, false, false); }
        //else { Play(E_Animations.Idle, LOWERBODY, false, false); }

        float _crouchMultiplier = 1f;
        Vector3 _direction = new(movementAxis.x, 0, movementAxis.y);

        //if (_direction.magnitude > movementThreshold)
        //{
        //    character_Rigidbody.velocity = Vector3.SmoothDamp(character_Rigidbody.velocity, _direction * walkSpeed * _crouchMultiplier, ref currentVelocity, dampSpeedUp);
        //}
        //else character_Rigidbody.velocity = Vector3.SmoothDamp(character_Rigidbody.velocity, Vector3.zero * _crouchMultiplier, ref currentVelocity, dampSpeedDown);


        if (_direction.magnitude > movementThreshold)
        {
            Character_Rigidbody.velocity = Vector3.SmoothDamp(Character_Rigidbody.velocity, _direction * currentMovementValues[(int)E_MovementValues.Speed_Walk] * _crouchMultiplier, ref currentVelocity, currentMovementValues[(int)E_MovementValues.Movement_DampUp]); // Normal Movement
        }
        else Character_Rigidbody.velocity = Vector3.SmoothDamp(Character_Rigidbody.velocity, Vector3.zero * _crouchMultiplier, ref currentVelocity, currentMovementValues[(int)E_MovementValues.Movement_DampDown]);

        // RotatePlayer();
    }
    //private void RotatePlayer()
    //{
    //    // Calculate the target rotation based on the movement axis
    //    Vector3 _axis = movementAxis;

    //    if (_axis.magnitude > movementThreshold)
    //    {
    //        float _turnAMT = Mathf.Clamp((rotationAmount * _axis.x), -90, 90);
    //        _turnAMT = _axis.y < 0 ? (_turnAMT * -1) + -180 : _axis.y >= 0 ? _turnAMT + 0f : _turnAMT + 0;

    //        Vector3 _turnDirection = new Vector3(0f, _turnAMT, 0f);
    //        _turnDirection -= Camera.main.transform.forward;

    //        Quaternion targetRotation = Quaternion.Euler(_turnDirection);

    //        // Smoothly interpolate between the current rotation and the target rotation
    //        Character_Visuals.transform.rotation = Quaternion.Lerp(Character_Visuals.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    //    }
    //}
    #endregion

    #region Keeping The Character Upright Methods
    private void ApplyHover()
    {
        // Cast a ray downwards to detect the ground
        RaycastHit hit;
        bool _rayHit = false;
        if (Physics.Raycast(transform.position, direction_Downward, out hit))
        {
            _rayHit = true;
        }
        if (_rayHit)
        {
            Vector3 _velocity = character_Rigidbody.velocity;
            Vector3 _rayDirection = transform.TransformDirection(Vector3.down);

            Vector3 _otherVelocity = Vector3.zero;
            Rigidbody _hitRigidbody = hit.rigidbody;
            if (_hitRigidbody != null) { _otherVelocity = _hitRigidbody.velocity; }

            float _rayDirectionVelocity = Vector3.Dot(_rayDirection, _velocity);
            float _otherDirectionVelocity = Vector3.Dot(_rayDirection, _otherVelocity);

            float _relativeVelocity = _rayDirectionVelocity - _otherDirectionVelocity;

            float _x = hit.distance - rideHeight;

            float _springForce = (_x * rideSpringStrength) - (_relativeVelocity * rideSpringDamp);

            Debug.DrawLine(transform.position, transform.position - (_rayDirection * _springForce), Color.yellow);

            character_Rigidbody.AddForce(_rayDirection * _springForce);

            if (_hitRigidbody != null)
            {
                _hitRigidbody.AddForceAtPosition(_rayDirection * -_springForce, hit.point);
            }
        }
    }

    private void UpdateUprightForce()
    {
        Quaternion _characterCurrent = transform.rotation;
        Quaternion _rotationGoal = GetShortestRotationQuaternion(_characterCurrent, Quaternion.Euler(new Vector3(0, transform.rotation.y, 0)));

        Vector3 _rotationAxis;
        float _rotationDegrees;

        _rotationGoal.ToAngleAxis(out _rotationDegrees, out _rotationAxis);
        _rotationAxis.Normalize();

        float _rotationRadians = _rotationDegrees * Mathf.Deg2Rad;

        character_Rigidbody.AddTorque((_rotationAxis * (_rotationRadians * uprightSpringStrength)) - (character_Rigidbody.angularVelocity * uprightSpringDamp));
    }

    public Quaternion GetShortestRotationQuaternion(Quaternion fromRotation, Quaternion toRotation)
    {
        // Convert both quaternions to Euler angles
        Vector3 fromEulerAngles = fromRotation.eulerAngles;
        Vector3 toEulerAngles = toRotation.eulerAngles;

        // Calculate the difference in Euler angles
        Vector3 deltaAngles = toEulerAngles - fromEulerAngles;

        // Normalize the angles to be within the range (-180, 180)
        deltaAngles.x = Mathf.Repeat(deltaAngles.x + 180f, 360f) - 180f;
        deltaAngles.y = Mathf.Repeat(deltaAngles.y + 180f, 360f) - 180f;
        deltaAngles.z = Mathf.Repeat(deltaAngles.z + 180f, 360f) - 180f;

        // Convert the difference back to a quaternion
        return Quaternion.Euler(deltaAngles);
    }
    #endregion
    #region Animation
    private void DefaultAnimation(int _layer)
    {
        if (_layer == UPPERBODY) { CheckTopAnimation(); }
        else { CheckBottomAnimation(); }
    }
    private void CheckTopAnimation() { CheckMovementAnimations(UPPERBODY); }
    private void CheckBottomAnimation() { CheckMovementAnimations(LOWERBODY); }
    private void CheckMovementAnimations(int _layer) { }
    #endregion

    #region Getters & Setters
    private void SetupMovementValues()
    {
        defaultMovementValues = new List<float>()
        {
            walkSpeed,
            sprintSpeed,
            rotationAmount,
            rotationSpeed,
            dampSpeedUp,
            dampSpeedDown
        };
        foreach (var value in defaultMovementValues) { currentMovementValues.Add(value); }
    }

    public void ResetMovementValues()
    {
        currentMovementValues.Clear();
        foreach (var value in defaultMovementValues) { currentMovementValues.Add(value); }
    }

    public float GetMovementValue(E_MovementValues _valueType) => currentMovementValues[(int)_valueType];
    public void SetMovementValue(E_MovementValues _valueType, float _value) => currentMovementValues[(int)_valueType] = _value;

    public void SetMovementValues(List<float> _newValues)
    {
        for (int i = 0; i < currentMovementValues.Count; i++)
        {
            currentMovementValues[i] = _newValues[i];
        }
    }

    public bool GetIsCurrentValuesDefault()
    {
        for (int i = 0; i < defaultMovementValues.Count; i++)
        {
            if (currentMovementValues[i] == defaultMovementValues[i]) { continue; }
            else return false;
        } return true;
    }

    #endregion
}


public enum E_MovementValues
{
    Speed_Walk,
    Speed_Sprint,
    Rotation_Degrees,
    Rotation_Speed,
    Movement_DampUp,
    Movement_DampDown,
}