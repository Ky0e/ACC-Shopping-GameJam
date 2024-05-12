using System;
using UnityEngine;
using NaughtyAttributes;
using ProjectStartup.ScriptableObjects.Variables;

public class Component_Health : MonoBehaviour, IDamageable
{
    [InfoBox("The Current Health Can Be Used As Either A Regular Float Or Stored In A Float Variable SO. See Documentation For More Details", EInfoBoxType.Normal)]
    [field: SerializeField, BoxGroup("Health Properties")] private float maxHealth = 100;
    [field: SerializeField, BoxGroup("Health Properties")] private FloatRefrence currentHealth;
    [field: SerializeField, BoxGroup("Debug Properties"), ProgressBar("Current Health", "maxHealth", EColor.Red)] private float debugHealthMeter;
    public Action<float> OnHealthChanged;

    [Button("Increase Health Test", EButtonEnableMode.Always)]
    public void IncreaseHealthButtonTest() => IncreaseHealth(5);
    [Button("Decrease Health Test", EButtonEnableMode.Always)]
    public void DecreaseHealthButtonTest() => DecreaseHealth(5);
    [Button("Reset Health Test", EButtonEnableMode.Always)]
    public void ResetHealthButtonTest() => SetHealth(0);


    /// <summary>
    /// Increases The Health By The Specified Amount
    /// </summary>
    /// <param name="_amount"></param>
    public void IncreaseHealth(float _amount)
    {
        currentHealth.Value += _amount;
        currentHealth.Value = Mathf.Clamp(currentHealth.Value, 0, maxHealth);
        debugHealthMeter = currentHealth.Value; // Can Be Deleted
        OnHealthChanged?.Invoke(currentHealth.Value);
    }

    /// <summary>
    /// Decreases The Health By The Specified Amount
    /// </summary>
    /// <param name="_amount"></param>
    public void DecreaseHealth(float _amount)
    {
        currentHealth.Value -= _amount;
        currentHealth.Value = Mathf.Clamp(currentHealth.Value, 0, maxHealth);
        debugHealthMeter = currentHealth.Value; // Can Be Deleted
        OnHealthChanged?.Invoke(currentHealth.Value);
    }

    public void SetHealth(float _amount)
    {
        currentHealth.Value = _amount;
        currentHealth.Value = Mathf.Clamp(currentHealth.Value, 0, maxHealth);
        debugHealthMeter = currentHealth.Value; // Can Be Deleted
        OnHealthChanged?.Invoke(currentHealth.Value);
    }

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("LET ME SEE THE MANAGER");
        DecreaseHealth(damageAmount);
    }

    public void SetMaxHealth(float _maxHealth)
    {
        maxHealth = _maxHealth;
    }
}
