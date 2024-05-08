using System;
using UnityEngine;
using NaughtyAttributes;

namespace ProjectStartup.ScriptableObjects.Variables
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Variables/Primatives/Float", fileName = "New Float", order = -1)]
    public class FloatVariable : ScriptableObject
    {
        [field: SerializeField] public float Value;
        [field: SerializeField, InfoBox("A Scriptable Object Refrence To A Constant Shared Float Value", EInfoBoxType.Normal), BoxGroup("Information"), Label("Extra Information"), ResizableTextArea] private string info;
    }

    [Serializable]
    public class FloatRefrence
    {
        public bool UseConstant = true;
        [field: SerializeField, AllowNesting, ShowIf("UseConstant")] public float ConstantValue;
        [field: SerializeField, AllowNesting, HideIf("UseConstant")] public FloatVariable Variable;

        public float Value { get { return UseConstant ? ConstantValue : Variable != null ? Variable.Value : 0; } }
    }

}