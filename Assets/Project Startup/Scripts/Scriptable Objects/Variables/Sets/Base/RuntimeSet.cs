using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

namespace ProjectStartup.ScriptableObjects.Variables
{
    public abstract class RuntimeSet<T> : ScriptableObject
    {
        [field: Expandable, AllowNesting] public List<T> Items = new();

        public void Add(T t)
        {
            if (!Items.Contains(t)) Items.Add(t);
        }
        public void Remove(T t)
        {
            if (Items.Contains(t)) Items.Remove(t);
        }
    }
}