using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet :  BulletBase<FireBullet>
{


    public override void Run(float dt)
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
