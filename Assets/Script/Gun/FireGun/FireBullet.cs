using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireBullet : BulletBase<FireBullet>
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bubble"))
        {
            var bubble = other.GetComponent<Bubble>();

            Explosion.Create(bubble.transform.position, bubble.size);
            bubble.Release();
        }

        if (other.CompareTag("Rock"))
        {
            Release();
        }
    }
}