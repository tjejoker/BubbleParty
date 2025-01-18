using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Framework;

public class FsmSample 
{
    // 1. 声明状态机变量
    // 2. 使用get(string key)函数添加状态机的具体状态
    // 3. 使用AddListener 函数 添加具体状态的具体事件( 枚举 FSMState)
    // 4 . Run(Update)   Jump(string key)


    private FsmAdvance _fsm;
    private float _hp = 100;
    
    // Start is called before the first frame update
    void Start()
    {

        _fsm = new FsmAdvance("idle");

        
        _fsm.Get("idle")
            .AddListener(FsmState.OnEnter, (() =>
            {
                Debug.Log("idle enter");
                _hp = 100;
            }))
            .AddListener(FsmState.OnExit, () =>
            {
                _hp -= Time.deltaTime;
                Debug.Log($"idle update, hp: {_hp:0.00}");
                if(_hp <= 0)
                    _fsm.Jump("dead");
            });


        _fsm.Get("dead")
            .AddListener("OnEnter", (() =>
            {
                Debug.Log("dead enter");
            }));


    }

    // Update is called once per frame
    void Update()
    {
        _fsm.Run();
    }
}
