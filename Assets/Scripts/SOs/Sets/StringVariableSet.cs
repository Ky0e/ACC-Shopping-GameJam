using ProjectStartup.ScriptableObjects.Variables;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Variables/Container Sets/String Variable Set", fileName = "New String Variable Set", order = 0)]

public class StringVariableSet : RuntimeSet<StringVariable>
{
    public void ResetItems() => Items.Clear();
}