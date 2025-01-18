using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Bubble : BulletBase<Bubble>
{
    public float size;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            player.bubbleDeBuff += size * 0.5f;
            Release();
        }
    }
}