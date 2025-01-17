using System;
using System.Collections.Generic;

namespace Framework
{
    public abstract class TimerBase : IUpdate
    {
        private bool _isDone = false;
        protected float DeltaTime;
        protected float Time;

        public abstract void Run(float dt);
        public virtual bool IsDone() => _isDone;
        public void Done() => _isDone = true;
    }

    /// <summary>
    /// x秒内一直执行
    /// </summary>
    public class TimerInterval : TimerBase
    {
        private Action _action;

        public static TimerInterval Create(float time, Action action)
        {
            var ti = new TimerInterval
            {
                Time = time,
                _action = action
            };

            GlobalUpdate.Instance.Register(ti);

            return ti;
        }

        public override void Run(float dt)
        {
            DeltaTime += dt;
            if (!(DeltaTime > Time)) return;
            _action.Invoke();
            Done();
        }
    }

    /// <summary>
    /// 隔x秒执行一次
    /// </summary>
    public class TimerRepeat : TimerBase
    {
        private Action _action;

        public static TimerRepeat Create(float time, Action action)
        {
            var tp = new TimerRepeat
            {
                Time = time,
                _action = action
            };
            GlobalUpdate.Instance.Register(tp);

            return tp;
        }


        public override void Run(float dt)
        {
            DeltaTime += dt;
            if (!(DeltaTime > Time)) return;
            _action.Invoke();
            DeltaTime = 0;
        }

        public void DoNow()
        {
            _action.Invoke();
        }
    }

    /// <summary>
    /// 在x秒后执行
    /// </summary>
    public class TimerRun : TimerBase
    {
        private Action<float> _action;
        private Action _onComplete;

        public static TimerRun Create(float time, Action<float> action)
        {
            var tr = new TimerRun
            {
                Time = time,
                _action = action
            };
            GlobalUpdate.Instance.Register(tr);

            return tr;
        }


        // ReSharper disable Unity.PerformanceAnalysis
        public override void Run(float dt)
        {
            DeltaTime += dt;
            _action.Invoke(DeltaTime);

            if (!(DeltaTime > Time)) return;

            _onComplete?.Invoke();
            Done();
        }

        public TimerRun Complete(Action action)
        {
            _onComplete += action;
            return this;
        }

        public void CompleteReset()
        {
            _onComplete = null;
        }
    }
}