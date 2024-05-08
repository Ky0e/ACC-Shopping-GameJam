using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using ProjectStartup.ScriptableObjects.Variables;

namespace ProjectStartup.ScriptableObjects.Helpers
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Helper Objects/General/Object Pool", fileName = "New Object Pool")]
    public class ObjectPool : ScriptableObject
    {
        [InfoBox("If Default Pool Size Is Zero, The Pool Will Self Populate At Runtime As Objects Are Needed", EInfoBoxType.Normal)]
        [field: SerializeField, BoxGroup("Object Pool Properties")] private int defaultPoolSize = 10;
        [field: SerializeField, BoxGroup("Object Pool Properties")] private GameObject objectPrefab;
        [field: SerializeField, BoxGroup("Object Pool Properties"), Expandable] private GameObjectSet objectSet;
        private Transform poolParent;
        [field: SerializeField, BoxGroup("Information"), InfoBox("A 'Pool' Of Objects To Be Used During Runtime", EInfoBoxType.Normal), Label("Extra Information"), ResizableTextArea] private string info;


        public void CreateObjectPool(Transform _poolParent)
        {
            if (_poolParent != null) { poolParent = _poolParent; }
            if (objectSet.Items.Count > 0) { objectSet.ResetItems(); }

            for (int i = 0; i < defaultPoolSize; i++) { AddObjectToPool(); }
        }

        private GameObject AddObjectToPool()
        {
            GameObject _createdOBJ;
            if (poolParent != null) { _createdOBJ = Instantiate(objectPrefab, poolParent); }
            else { _createdOBJ = Instantiate(objectPrefab); }
            objectSet.Add(_createdOBJ);
            _createdOBJ.SetActive(false);

            return _createdOBJ;
        }

        public GameObject GetPooledObject()
        {
            for (int i = 0; i < objectSet.Items.Count; i++)
            {
                if (!objectSet.Items[i].activeInHierarchy) { return objectSet.Items[i]; }
            }
            AddObjectToPool();
            return objectSet.Items[objectSet.Items.Count - 1];
        }

        public List<GameObject> GetActiveObjects()
        {
            List<GameObject> _activeObjects = new();

            foreach (var _item in objectSet.Items)
            {
                if (!_item.activeInHierarchy) { continue; }
                else { _activeObjects.Add(_item); }
            }
            Debug.Log($"NUMBER OF ACTIVE OBJECTS {_activeObjects.Count}");
            return _activeObjects;
        }

        public int GetPoolSize => objectSet.Items.Count;
    }
}
