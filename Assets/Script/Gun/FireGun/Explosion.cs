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


    private float _size;
    
    private void OnEnable()
    {

        TimerInterval.Create(0.05f, () =>
        {
            Pool.Destroy(this);
        });
    }

    public static void Create(Vector3 pos, float size)
    {
        var ex = Pool.Create();
        ex.transform.position = pos;
        ex.transform.localScale = Vector3.one * size;
        
        ex._size = size;
        // TODO: 播放音效
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            player.hp -= _size * 0.1f;
            if (player.bubbleDeBuff > 0.5f)
            {
                Create(player.transform.position, player.bubbleDeBuff);
                player.bubbleDeBuff = 0;
            }
        }
        
        if (other.CompareTag("Bubble"))
        {
            var bubble = other.GetComponent<Bubble>();
            if(bubble.IsDone())
                return;
            
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
 
}
