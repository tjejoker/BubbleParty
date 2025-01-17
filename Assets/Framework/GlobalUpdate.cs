using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public interface IUpdate
    {
        void Run(float dt);
        bool IsDone();
    }
    public class GlobalUpdate : MonoSingleton<GlobalUpdate>
    {
        public float worldTimeScale = 1;
        private readonly DoubleEventBuffer _buffer = new();

        public void Register(IUpdate timer)
        {
            _buffer.Register(timer);
        }

        // Update is called once per frame
        public void Update()
        {
            _buffer.Run(Time.deltaTime * worldTimeScale);
        }
    }


    public class DoubleEventBuffer
    {
        private readonly Queue<IUpdate> _first = new();
        private readonly Queue<IUpdate> _last = new();

        private bool _isFirst = true;

        private Queue<IUpdate> GetRunning()
        {
            return _isFirst ? _first : _last;
        }

        private Queue<IUpdate> GetSpare()
        {
            return _isFirst ? _last : _first;
        }

        public void Run(float deltaTime)
        {

            while (GetRunning().Count > 0)
            {
                var execute = GetRunning().Dequeue();
                execute.Run(deltaTime);

                if(!execute.IsDone())
                    GetSpare().Enqueue(execute);

            }

            _isFirst = !_isFirst;
        }

        public void Register(IUpdate timer)
        {
            GetRunning().Enqueue(timer);
        }

    }
}