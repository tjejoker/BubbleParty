using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class BufferPool<T> where T : MonoBehaviour
    {
        private readonly GameObject _manager;
        private readonly GameObject _prefab;
        private readonly Stack<T> _pools = new();

        protected BufferPool(){}
        public BufferPool(GameObject prefab)
        {
            _manager = new GameObject(prefab.name + "Pool");
            _prefab = prefab;
        }

        /// <summary>
        /// activated from the bool. create a new by prefab if the pool is empty
        /// need set pos and rot and parent after created.
        /// </summary>
        /// <returns></returns>
        public T  Create()
        {
            if (_pools.Count == 0)
                return Object.Instantiate(_prefab).GetComponent<T>();

            var behaviour= _pools.Pop();
            var obj = behaviour.gameObject;
            obj.SetActive(true);
            
            return behaviour;
        }

        public void Destroy(T obj)
        {
            obj.transform.SetParent(_manager.transform, false);
            obj.gameObject.SetActive(false);
            _pools.Push(obj);
        }

    }
}
