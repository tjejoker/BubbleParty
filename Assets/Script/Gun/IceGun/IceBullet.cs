using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet :  BulletBase<IceBullet>
{
    
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
