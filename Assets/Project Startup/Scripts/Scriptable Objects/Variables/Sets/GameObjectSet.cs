using NaughtyAttributes;
using UnityEngine;

namespace ProjectStartup.ScriptableObjects.Variables
{
    /// <summary>
    /// A Scriptable Object List Of GameObjects
    /// </summary>
    [CreateAssetMenu(menuName = "Scriptable Objects/Variables/Container Sets/GameObject Set", fileName = "New GameObject Set", order = 0)]

    public class GameObjectSet : RuntimeSet<GameObject>
    {
        [field: SerializeField, InfoBox("Scriptable Object List Of GameObjects", EInfoBoxType.Normal), BoxGroup("Information"), Label("Extra Information"), ResizableTextArea] private string info;
        public void ResetItems() => Items.Clear();
    }
}