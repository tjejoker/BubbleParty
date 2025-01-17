using UnityEngine;

namespace Framework
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected virtual string PrefabPath { get; } = default;
        public static T Instance
        {
            get
            {

                if (_instance != null && _instance.gameObject == null)
                    _instance = null;

                if (_instance) return _instance;
            
                var obj = new GameObject();
                var sington = obj.AddComponent<T>();
                if (sington.PrefabPath == default)
                {
                    _instance = sington;
                    obj.name = Instance.GetType().Name;
                }
                else
                {
                    var prefab = AssetLoder.AssetLoad<GameObject>(sington.PrefabPath);
                    Destroy(obj);

                    _instance = GameObject.Instantiate(prefab).GetComponent<T>();
                }

                return _instance;
            }
        }

        private static T _instance = null;

        public virtual void Awake()
        {
            if ((_instance != null && _instance.gameObject == null) || _instance == null)
                _instance = this as T;
        }


        public virtual void Discard()
        {
            Destroy(gameObject);
            _instance = null;
        }


    }
}

