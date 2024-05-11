using System;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Scriptable Objects/Variables/Primatives/String", fileName = "New String", order = -1)]
public class StringVariable : ScriptableObject
{
    [field: SerializeField, AllowNesting, ResizableTextArea] public string Value;
}

[Serializable]
public class StringRefrence
{
    public bool UseConstant = true;
    [field: SerializeField, AllowNesting, ShowIf("UseConstant"), ResizableTextArea] public string ConstantValue;
    [field: SerializeField, AllowNesting, HideIf("UseConstant")] public StringVariable Variable;

    public string Value { get { return UseConstant ? ConstantValue : Variable != null ? Variable.Value : ""; } }
}
