using System;
using UnityEngine;
using System.Collections.Generic;
using ProjectStartup.ScriptableObjects.Helpers;

namespace ProjectStartup.Helpers
{ 
    public class ObjectPoolCreator : MonoBehaviour
    {
        [field: SerializeField] private List<ObjectPoolGroup> objectPools;

        private void Start()
        {
            foreach (var _pool in objectPools)
            {
                _pool.ObjectPoolSO.CreateObjectPool(_pool.PoolParent);
            }
        }
    }

    [Serializable]
    public class ObjectPoolGroup
    {
        [field: SerializeField] private string name;
        [field: SerializeField] public ObjectPool ObjectPoolSO;
        [field: SerializeField] public Transform PoolParent;
    }
}
