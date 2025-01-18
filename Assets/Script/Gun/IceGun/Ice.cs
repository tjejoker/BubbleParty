using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class Ice : MonoBehaviour
{
    private static BufferPool<Ice> _pool;

    private static BufferPool<Ice> Pool
        => _pool ??= new BufferPool<Ice>(Resources.Load<GameObject>($"Ice"));


    private float _size;

    public float Size
    {
        get => _size;
        set
        {
            _size = value;
            transform.localScale = new Vector3(_size, _size, _size);
            
            if (_size < 0.1f)
            {
                Destroy();
            } 
        }
    }


    private void Update()
    {
        Size -= Time.deltaTime;
    }

    public static void Create(Vector3 position, float size)
    {
        var ice = Pool.Create();
        ice.transform.position = new Vector3(position.x, position.y, 0);
        ice.transform.localScale = new Vector3(size, size, size);
        
        ice.Size = size;
        // TODO: 播放音效
        AudioManager.Instance.PlayEffect(ResSvc.Instance.GetAudioClip("结冰/结冰"));
    }


    private void Destroy()
    {
        Pool.Destroy(this);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            player.inIce = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            player.inIce = false;
        }
        
    }
}
