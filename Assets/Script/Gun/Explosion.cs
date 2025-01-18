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


    private void OnEnable()
    {


        TimerInterval.Create(0.05f, () =>
        {
            Pool.Destroy(this);
        });
    }

    private void OnDisable()
    {
        
    }


    public static void Create(Vector2 pos, float size)
    {
        var ex = Pool.Create();
        ex.transform.position = pos;
        ex.transform.localScale = Vector3.one * size;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
        }
        
        if (other.CompareTag("Bubble"))
        {
            var bubble = other.GetComponent<Bubble>();
            if(bubble.IsDone())
                return;
            
            bubble.Destroy();
            // 爆炸预制体
            Create(bubble.transform.position, bubble.size * 1.3f);
        }
    }
}
