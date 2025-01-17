using System;

namespace Framework
{
    public class Singleton<T> where T : Singleton<T>, new()
    {
        // ReSharper disable once InconsistentNaming
        private static readonly Lazy<T> m_instance = new(() => { var t = new T(); t.OnCreate(); return t; });
        public static T Instance => m_instance.Value;

        protected virtual void OnCreate() { }

    }
}
