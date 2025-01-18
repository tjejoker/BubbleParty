using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class IceBullet :  BulletBase<IceBullet>
{
    
    private Vector3 _direction;
    private float _speed;

    public void Initialize(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
        GlobalUpdate.Instance.Register(this);
    }

    public override void Run(float dt)
    {
        transform.position += _direction * (_speed * dt);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player.bubbleDeBuff > 0.3f)
            {
                Debug.Log("对玩家施加了减速DeBuff");
                var buff = player.GetDeBuff<FreezeDeBuff>(FreezeDeBuff.Key);
                buff.Set(player, player.bubbleDeBuff);
                player.hp -= 1 + player.bubbleDeBuff * 0.05f;
                Ice.Create(player.transform.position, player.bubbleDeBuff*0.5f);
                player.bubbleDeBuff = 0;
            }
        }
        
        if (other.CompareTag("Bubble"))
        {
            var bubble = other.GetComponent<Bubble>();
            Ice.Create(bubble.transform.position, bubble.size);
            bubble.Release();
        }

        if (other.CompareTag("Rock"))
        {
            Release();
        }
    }
}
