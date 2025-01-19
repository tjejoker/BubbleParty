using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private static BufferPool<Explosion> _pool;

    private static BufferPool<Explosion> Pool
        => _pool ??= new BufferPool<Explosion>(Resources.Load<GameObject>($"Explosion"));


    //public float factor = 10f;
    private float _size;

    private void OnEnable()
    {
        TimerInterval.Create(0.5f, () => { Pool.Destroy(this); });
    }

    public static void Create(Vector3 pos, float size)
    {
        var ex = Pool.Create();
        ex.transform.position = pos;
        ex.transform.localScale = Vector3.one * size * 5f;

        ex._size = size;

        // TODO: 播放音效

        // TODO: 播放特效

        // TODO: 代替碰撞检测
        var range = size * 1.5f;
        var results = new Collider2D[16];
        var s = Physics2D.OverlapCircleNonAlloc(ex.transform.position, range, results);
        for (var i = 0; i < s; i++)
        {
            ex.CheckCollider(results[i]);
        }
    }


    private void CheckCollider(Collider2D other)
    {
        if(other.gameObject == gameObject) 
            return;

        var max = _size * 2;
        var distance = Vector2.Distance(other.transform.position, transform.position);
        var orientation = (other.transform.position - transform.position).normalized;
        var distanceFactor = distance / max;

        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            // 根据 距离 and 大小 造成伤害
            const float damageFactor = 30f;

            player.hp -= damageFactor * distanceFactor;
//            player.AddStrikeForce(orientation * _size * distanceFactor);

            // 玩家身上的爆炸
            if (player.bubbleDeBuff > 0.5f)
            {
                // 防止递归
                var value = player.bubbleDeBuff;
                player.bubbleDeBuff = 0;
                Create(player.transform.position, value);
            }
        }

        if (other.CompareTag("Bubble") && other.gameObject.activeSelf)
        {
            var bubble = other.GetComponent<Bubble>();
            bubble.Release();
            // 爆炸预制体
            Create(bubble.transform.position, bubble.size * 1.3f);
        }

        if (other.CompareTag("Ice"))
        {
            var ice = other.GetComponent<Ice>();
            ice.Size -= _size * 0.3f;
        }

        if (other.CompareTag("Rock"))
        {
            var rock = other.GetComponent<Rock>();
            rock.Hp -= _size * 0.5f;
        }
    }


    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     
    //     if (other.CompareTag("Player"))
    //     {
    //         var player = other.GetComponent<Player>();
    //         player.hp -= _size * 0.1f;
    //         if (player.bubbleDeBuff > 0.5f)
    //         {
    //             Create(player.transform.position, player.bubbleDeBuff);
    //             player.bubbleDeBuff = 0;
    //         }
    //     }
    //     
    //     if (other.CompareTag("Bubble"))
    //     {
    //         var bubble = other.GetComponent<Bubble>();
    //         if(bubble.IsDone())
    //             return;
    //         
    //         bubble.Release();
    //         // 爆炸预制体
    //         Create(bubble.transform.position, bubble.size * 1.3f);
    //     }
    //
    //     if (other.CompareTag("Ice"))
    //     {
    //         var ice = other.GetComponent<Ice>();
    //         ice.Size -= _size * 0.3f;
    //         
    //     }
    //
    //     if (other.CompareTag("Rock"))
    //     {
    //         var rock = other.GetComponent<Rock>();
    //         rock.Hp -= _size * 0.5f;
    //     }
    // }
}