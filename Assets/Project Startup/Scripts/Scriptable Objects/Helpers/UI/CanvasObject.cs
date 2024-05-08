using NaughtyAttributes;
using UnityEngine;

namespace ProjectStartup.ScriptableObjects.Helpers
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Helper Objects/UI/Canvas Object", fileName = "New Canvas Object")]
    public class CanvasObject : ScriptableObject
    {
        [field: SerializeField, BoxGroup("Canvas Properties")] public string CanvasName;
        [field: SerializeField, BoxGroup("Canvas Properties")] private GameObject canvasPrefab;
        [field: SerializeField, InfoBox("An Object Representing The Canvas UI To Be Spawned At Runtime", EInfoBoxType.Normal), BoxGroup("Information"), Label("Extra Information"), ResizableTextArea] private string info;

        public void InstantiateCanvas() { Instantiate(canvasPrefab); }
        public void InstantiateCanvas(Transform _parent) { Instantiate(canvasPrefab, _parent); }
        public void InstantiateCanvas(Vector3 _position, Quaternion _rotation, Transform _parent) { Instantiate(canvasPrefab, _position, _rotation, _parent); }
    }
}
