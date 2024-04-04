using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Quiz.Factory
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly List<T> _pool = new List<T>();
        private readonly T _prefab;
        private readonly Transform _parrent;

        public ObjectPool(T prefab)
        {
            _prefab = prefab;
            _parrent = new GameObject("ObjectPool").transform;
        }

        public T Get()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].gameObject.activeInHierarchy)
                    continue;

                return _pool[i];
            }
            return Create();
        }

        public void Dispose()
        {
            for (int i = 0; i < _pool.Count; i++)
                if (_pool[i])
                    Object.Destroy(_pool[i]);
            _pool.Clear();
        }

        private T Create()
        {
            T newObject = Object.Instantiate(_prefab, Vector3.zero, Quaternion.identity, _parrent);
            newObject.gameObject.SetActive(false);
            _pool.Add(newObject);
            return newObject;
        }
    }
}
