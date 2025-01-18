using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class BubbleTeaBullet : MonoBehaviour,IUpdate
{
    private static BufferPool<BubbleTeaBullet> _pool;

    private static BufferPool<BubbleTeaBullet> Pool
        => _pool ??= new BufferPool<BubbleTeaBullet>(Resources.Load<GameObject>($"BubbleTeaBullet"));


    public static BubbleTeaBullet Create(GunBase gun)
    {
        var bullet = Pool.Create();

        bullet.transform.position = gun.shootPos.position;
        bullet.transform.rotation = gun.shootPos.rotation;

        GlobalUpdate.Instance.Register(bullet);

        return bullet;
    }


    public void Run(float dt)
    {
        // 本体向前移动
        
        // 沿途留下奶茶

        // 飞行一定距离消失
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 碰到玩家或者ROCK 爆开 消失
        
        // 消除冰面
        
    }


    public bool IsDone()
    {
        return !isActiveAndEnabled;
    }
    
    private void Destroy()
    {
        Pool.Destroy(this);
    }
}
