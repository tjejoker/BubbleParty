using System;
using System.Collections.Generic;

namespace Framework
{
    public static class FsmState
    {
        public const string OnCreate = "OnCreate";
        public const string OnEnter = "OnEnter";
        public const string OnUpdate = "OnUpdate";
        public const string OnExit = "OnExit";
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class FsmAdvance
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public string Current { get; private set; }
        private readonly Dictionary<string, FsmAction> _states = new();
        private readonly Queue<string> _records = new();
        
        private readonly Dictionary<string, Action> _defaultActions = new();
        
        public class FsmAction
        {
            public string State;
            public FsmAdvance Fsm;
            private readonly Dictionary<string, Action> _actions = new();

            // ReSharper disable once MemberCanBeProtected.Global
            public FsmAction AddListener(string key, Action action)
            {
                if (!_actions.TryAdd(key, action))
                    _actions[key] += action;

                return this;
            }

            public Action Get(string key)
            {
                var a =_actions.GetValueOrDefault(key); 
                return a ?? Fsm._defaultActions.GetValueOrDefault(key);
            }

            public void Jump()
            {
                Fsm.Jump(State);
            }
        }
        protected FsmAdvance() { }

        public FsmAdvance(object current)
        {
            this.Current = current.ToString();
        }
        // ----------------Get------------------------

        public FsmAction Get(object state)
        {
            return Get<FsmAction>(state);
        }
        // ReSharper disable once MemberCanBePrivate.Global
        public T Get<T>(object state) where T : FsmAction, new()
        {
            var key = state.ToString();
            
            if (_states.TryGetValue(key, out var s)) 
                return s as T;
            
            var ns = new T()
            {
                Fsm = this,
                State = key
            };

            ns.Get(FsmState.OnCreate)?.Invoke();

            _states.Add(key, ns);

            return _states[key] as T;
        }

        public T GetCurrent<T>() where T : FsmAction
        {
            return _states[Current] as T;
        }

        // ----------------Jump------------------------
        // ReSharper disable once MemberCanBePrivate.Global
        public void Jump(object state)
        {
            _states[Current].Get(FsmState.OnExit)?.Invoke();
            Current = state.ToString();
            _states[Current].Get(FsmState.OnEnter)?.Invoke();
        }
        // ----------------Record------------------------

        public void Record()
        {
            _records.Enqueue(Current);
        }

        public bool Restore()
        {
            if (_records.Count <= 0) return false;
            
            Jump(_records.Dequeue());
            
            return true;
        }

        // ----------------Client------------------------ 

        public FsmAdvance AddDefaultAction(string key, Action action)
        {
            if (!_defaultActions.TryAdd(key, action))
                _defaultActions[key] += action;

            return this;
        }
        
        public void Invoke(string e) => _states[Current].Get(e)?.Invoke();

        public void Run() => _states[Current].Get(FsmState.OnUpdate)?.Invoke();
    }


    public abstract class StateBase : FsmAdvance.FsmAction
    {
        protected StateBase()
        {
            AddListener(FsmState.OnCreate, OnCreate);
            AddListener(FsmState.OnEnter, OnEnter);
            AddListener(FsmState.OnUpdate, OnUpdate);
            AddListener(FsmState.OnExit, OnExit);
        }

        protected virtual void OnCreate() { }
        protected virtual void OnEnter() { }
        protected virtual void OnUpdate() { }
        protected virtual void OnExit() { }
    }
}